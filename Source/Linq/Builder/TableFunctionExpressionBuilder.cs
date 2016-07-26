using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	class TableFunctionExpressionBuilder : TableExpressionBuilder
	{
		public TableFunctionExpressionBuilder(QueryBuilder builder, Expression expression)
			: base(expression)
		{
		}
	}
}