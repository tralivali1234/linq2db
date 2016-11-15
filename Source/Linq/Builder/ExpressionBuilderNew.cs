using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	abstract class ExpressionBuilderNew
	{
		protected ExpressionBuilderNew(QueryBuilder queryBuilder, Expression expression)
		{
			QueryBuilder = queryBuilder;
			Expression   = expression;
		}

		public QueryBuilder QueryBuilder    { get; set; }
		public Expression   Expression { get; }

		public ExpressionBuilderNew Prev { get; set; }
		public ExpressionBuilderNew Next { get; set; }
		public Type              Type => Expression.Type;

		public abstract SelectQuery BuildSql<T>            (QueryBuilder<T> builder, SelectQuery selectQuery);
		public abstract void        BuildQuery<T>          (QueryBuilder<T> builder);
		public abstract Expression  BuildQueryExpression<T>(QueryBuilder<T> builder);
	}
}
