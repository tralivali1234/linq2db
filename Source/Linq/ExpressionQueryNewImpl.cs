using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq
{
	class ExpressionQueryImplNew<T> : ExpressionQueryNew<T>
	{
		public ExpressionQueryImplNew(IDataContextEx dataContext, Expression expression)
			: base(dataContext, expression)
		{
		}

		public override string ToString()
		{
			return SqlText;
		}
	}
}
