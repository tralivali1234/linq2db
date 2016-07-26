using System;

namespace LinqToDB.Linq.Builder
{
	using SqlQuery;

	class ProjectionSqlQueryBuilder : SqlQueryBuilder
	{
		public ProjectionSqlQueryBuilder(QueryBuilder queryBuilder, SelectQuery selectQuery)
			: base(queryBuilder)
		{
			SelectQuery = selectQuery;
		}
	}
}
