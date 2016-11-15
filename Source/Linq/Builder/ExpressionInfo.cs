using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	class ExpressionInfo
	{
		public ExpressionInfo(ExpressionBuilderNew expressionBuilder, LambdaExpression lambdaExpression)
		{
			ExpressionBuilder = expressionBuilder;
			Expression        = lambdaExpression;

			foreach (var p in lambdaExpression.Parameters)
			{
				Items[p] = new ExpressionItemInfo(p);
			}
		}

		public ExpressionBuilderNew                         ExpressionBuilder;
		public Expression                                Expression;
		public Dictionary<Expression,ExpressionItemInfo> Items = new Dictionary<Expression,ExpressionItemInfo>();
	}
}
