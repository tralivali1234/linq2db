using System;
using System.Linq.Expressions;

using LinqToDB.Linq;

namespace LinqToDB
{
	interface IDataContextEx : IDataContext
	{
		IQueryContextNew GetQueryContext(QueryNew query, Expression expression);
	}
}
