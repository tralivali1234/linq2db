using System;
using System.Linq.Expressions;

using LinqToDB.Expressions;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	class SelectExpressionBuilder : ExpressionBuilderNew
	{
		public static QueryExpression Translate(QueryBuilder builder, QueryExpression qe, MethodCallExpression expression)
		{
			if (expression.Arguments.Count != 2)
				throw new LinqToDBException($"Can't process expression:\r\n{expression.GetDebugView()}");

			var l = (LambdaExpression)expression.Arguments[1].Unwrap();

			if (l.Parameters.Count == 1 && l.Body == l.Parameters[0])
				return qe;

			return qe.AddBuilder(new SelectExpressionBuilder(builder, expression));
		}

		SelectExpressionBuilder(QueryBuilder queryBuilder, Expression expression)
			: base(queryBuilder, expression)
		{
		}

		SelectQuery _selectQuery;

		public override SelectQuery BuildSql<T>(QueryBuilder<T> builder, SelectQuery selectQuery)
		{
			return _selectQuery = selectQuery;
		}

		public override void BuildQuery<T>(QueryBuilder<T> builder)
		{
			throw new NotImplementedException();
		}

		public override Expression BuildQueryExpression<T>(QueryBuilder<T> builder)
		{
			throw new NotImplementedException();
		}
	}
}