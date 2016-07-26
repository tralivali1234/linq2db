using System;

namespace LinqToDB.Linq.Builder
{
	class SqlQueryBuilder
	{
		public SqlQueryBuilder(QueryBuilder queryBuilder)
		{
			QueryBuilder = queryBuilder;
		}

		public QueryBuilder QueryBuilder { get; }
	}
}
