namespace BuildingBlocks.Pagination
{
    public class PaginationResult<TEntity>
        (int pageIndex , int pageSize, long count , IEnumerable<TEntity> date)  where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEntity> Data { get; } = date;

    }
}
