using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	class ExpressionItemInfo
	{
		public ExpressionItemInfo(Expression expression)
		{
			Expression = expression;
		}

		public ExpressionFlags Flags = ExpressionFlags.None;
		public Expression      Expression;
	}
}
