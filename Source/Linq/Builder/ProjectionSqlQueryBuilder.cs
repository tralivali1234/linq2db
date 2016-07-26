using System;

namespace LinqToDB.Linq.Builder
{
	class ProjectionSqlQueryBuilder : SqlQueryBuilder
	{
		public ProjectionSqlQueryBuilder(QueryBuilder queryBuilder)
			: base(queryBuilder)
		{
		}
	}
}
