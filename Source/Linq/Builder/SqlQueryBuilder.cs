using System;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	abstract class SqlQueryBuilder
	{
		protected SqlQueryBuilder(QueryBuilder queryBuilder)
		{
			QueryBuilder = queryBuilder;
		}

		public QueryBuilder QueryBuilder { get; }
		public SelectQuery  SelectQuery  { get; protected set; }
	}
}
