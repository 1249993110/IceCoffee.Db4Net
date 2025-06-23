using IceCoffee.Db4Net.Core.SqlAdapters;

namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public class SqlQueryPagedBuilder : SqlQueryBuilderBase<SqlQueryPagedBuilder>
    {
        public SqlQueryPagedBuilder(ISqlAdapter sqlAdapter) : base(sqlAdapter)
        {
        }

        protected override SqlResult GetSqlResult()
        {
            var sqlResult = base.GetSqlResult();
            sqlResult.Sql += SqlAdapter.PagingCommand(_pageIndex, _pageSize);
            sqlResult.AttachedSql = SqlAdapter.CountCommand(GetFromTarget(), GetWhereConditions());
            return sqlResult;
        }

        #region Paging
        private int _pageIndex;
        private int _pageSize;
        public SqlQueryPagedBuilder PageNumber(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            }

            _pageIndex = pageNumber - 1;
            return this;
        }
        public SqlQueryPagedBuilder PageSize(int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            }

            _pageSize = pageSize;
            return this;
        }
        #endregion
    }
}
