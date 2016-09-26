using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToDB.Linq
{
	using Builder;
	using Common;
	using Data;
	using LinqToDB.Expressions;
	using Mapping;
	using SqlProvider;
	using SqlQuery;

	abstract class QueryNew
	{
		protected QueryNew(IDataContext dataContext, Expression expression)
		{
			ContextID       = dataContext.ContextID;
			Expression      = expression;
			MappingSchema   = dataContext.MappingSchema;
			ConfigurationID = dataContext.MappingSchema.ConfigurationID;
			SqlOptimizer    = dataContext.GetSqlOptimizer();
		}

		public readonly string        ContextID;
		public readonly Expression    Expression;
		public readonly MappingSchema MappingSchema;
		public readonly string        ConfigurationID;
		public readonly ISqlOptimizer SqlOptimizer;

		public SqlQuery SqlQuery;

		readonly Dictionary<Expression,QueryableAccessor> _queryableAccessorDic  = new Dictionary<Expression,QueryableAccessor>();

		public bool Compare(string contextID, MappingSchema mappingSchema, Expression expr)
		{
			return
				ContextID.Length == contextID.Length              &&
				ContextID        == contextID                     &&
				ConfigurationID  == mappingSchema.ConfigurationID &&
				Expression.EqualsTo(expr, _queryableAccessorDic);
		}

		public void FinalizeQuery(SqlQuery sqlQuery)
		{
			SqlQuery = SqlOptimizer.Finalize(sqlQuery);

			if (!SqlQuery.IsParameterDependent)
			{
				
			}
		}

		public class CommandInfo
		{
			public string          CommandText;
			public DataParameter[] Parameters;
		}

		public CommandInfo GetCommandInfo(IDataContext dataContext, Expression expression)
		{
			var sqlProvider   = dataContext.CreateSqlProvider();
			var stringBuilder = new StringBuilder();

			sqlProvider.BuildSql(0, SqlQuery, stringBuilder);

			return new CommandInfo
			{
				CommandText = stringBuilder.ToString(),
			};
		}
	}

	class QueryNew<T> : QueryNew
	{
		QueryNew(IDataContext dataContext, Expression expression)
			: base(dataContext, expression)
		{
		}

		public Func<IDataContextEx,Expression,T>                                GetElement;
		public Func<IDataContextEx,Expression,IEnumerable<T>>                   GetIEnumerable;
		public Func<IDataContextEx,Expression,Action<T>,CancellationToken,Task> GetForEachAsync;

		QueryNew<T> _next;

		#region GetQuery

		static          QueryNew<T> _first;
		static readonly object   _sync = new object();

		const int CacheSize = 100;

		public static QueryNew<T> GetQuery(IDataContext dataContext, Expression expr, bool isEnumerable)
		{
			var query = FindQuery(dataContext, expr);

			if (query == null)
			{
				lock (_sync)
				{
					query = FindQuery(dataContext, expr);

					if (query == null)
					{
						if (Configuration.Linq.GenerateExpressionTest)
						{
							var testFile = new ExpressionTestGenerator().GenerateSource(expr);
#if !SILVERLIGHT && !NETFX_CORE
							DataConnection.WriteTraceLine(
								"Expression test code generated: '" + testFile + "'.",
								DataConnection.TraceSwitch.DisplayName);
#endif
						}

						query = new QueryNew<T>(dataContext, expr);

						try
						{
							if (isEnumerable) query.GetIEnumerable = new QueryBuilder<T>(dataContext, query).BuildEnumerable();
							else              query.GetElement     = new QueryBuilder<T>(dataContext, query).BuildElement   ();
						}
						catch
						{
							if (!Configuration.Linq.GenerateExpressionTest)
							{
#if !SILVERLIGHT && !NETFX_CORE
								DataConnection.WriteTraceLine(
									"To generate test code to diagnose the problem set 'LinqToDB.Common.Configuration.Linq.GenerateExpressionTest = true'.",
									DataConnection.TraceSwitch.DisplayName);
#endif
							}

							throw;
						}
					}
				}
			}

			return query;
		}

		static QueryNew<T> FindQuery(IDataContext dataContext, Expression expr)
		{
			QueryNew<T> prev = null;
			var      n    = 0;

			for (var query = _first; query != null; query = query._next)
			{
				if (query.Compare(dataContext.ContextID, dataContext.MappingSchema, expr))
				{
					if (prev != null)
					{
						lock (_sync)
						{
							prev._next  = query._next;
							query._next = _first;
							_first      = query;
						}
					}

					return query;
				}

				if (n++ >= CacheSize)
				{
					query._next = null;
					return null;
				}

				prev = query;
			}

			return null;
		}

		#endregion

		#region Execute

		IEnumerable<T> ExecuteQuery(
			IDataContextEx dataContext,
			Mapper         mapper,
			Expression     expression)
		{
			Func<IDataReader,T> m = mapper.Map;

			using (var ctx = dataContext.GetQueryContext(this, expression))
			{
				mapper.QueryContext = ctx;

				var count = 0;

				using (var dr  = ctx.ExecuteReader())
				{
					while (dr.Read())
					{
						yield return m(dr);
						count++;
					}
				}

				ctx.RowsCount = count;
			}
		}

		async Task ExecuteQueryAsync(
			IDataContextEx    dataContext,
			Mapper            mapper,
			Expression        expression,
			Action<T>         action,
			CancellationToken cancellationToken)
		{
			Func<IDataReader,T> m = mapper.Map;

			using (var ctx = dataContext.GetQueryContext(this, expression))
			{
				mapper.QueryContext = ctx;
				using (var dr  = await ctx.ExecuteReaderAsync(cancellationToken))
					await dr.QueryForEachAsync(m, action, cancellationToken);
			}
		}

		class Mapper
		{
			public Mapper(Expression<Func<IDataReader,T>> mapperExpression)
			{
				_expression = mapperExpression;
			}

			readonly Expression<Func<IDataReader,T>> _expression;

			Expression<Func<IDataReader,T>> _mapperExpression;
			Func<IDataReader,T> _mapper;
			bool _isFaulted;

			public IQueryContextNew QueryContext;

			public T Map(IDataReader dataReader)
			{
				if (_mapper == null)
				{
					_mapperExpression = (Expression<Func<IDataReader,T>> )_expression.Transform(e =>
					{
						var ex = e as ConvertFromDataReaderExpression;
						return ex?.Reduce(dataReader) ?? e;
					});

					QueryContext.MapperExpression = _mapperExpression;

					_mapper = _mapperExpression.Compile();
				}

				try
				{
					return _mapper(dataReader);
				}
				catch (FormatException)
				{
					if (_isFaulted)
						throw;

					_isFaulted = true;

					QueryContext.MapperExpression = _expression;

					return (_mapper = _expression.Compile())(dataReader);
				}
				catch (InvalidCastException)
				{
					if (_isFaulted)
						throw;

					_isFaulted = true;

					QueryContext.MapperExpression = _expression;

					return (_mapper = _expression.Compile())(dataReader);
				}
			}
		}

		public void BuildQuery(Expression<Func<IDataReader,T>> mapper)
		{
			var m = new Mapper(mapper);

			GetIEnumerable  = (ctx, expr)                => ExecuteQuery     (ctx, m, expr);
			GetForEachAsync = (ctx, expr, action, token) => ExecuteQueryAsync(ctx, m, expr, action, token);
		}

		public Expression BuildQueryExpression(
			Expression<Func<IDataReader,T>> mapper,
			ParameterExpression dataContextParameter,
			ParameterExpression expressionParameter)
		{
			var expr = Expression.Call(
				Expression.Constant(this),
				MemberHelper.MethodOf(() => ExecuteQuery(null, null, null)),
				dataContextParameter,
				Expression.Constant(new Mapper(mapper)),
				expressionParameter,
				mapper);

			return expr;
		}

		#endregion
	}
}
