using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	class TableFunctionExpressionBuilder : TableExpressionBuilder
	{
		public TableFunctionExpressionBuilder(QueryBuilder queryBuilder, Expression expression)
			: base(queryBuilder, expression)
		{
		}
	}
}