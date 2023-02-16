namespace Utilities.CustomModels
{
    public class PagedList
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalPages { get; set; }
        public long MaxCount { get; set; }
        public long RecordsFrom { get; set; }
        public long RecordsTo { get; set; }
    }
}
