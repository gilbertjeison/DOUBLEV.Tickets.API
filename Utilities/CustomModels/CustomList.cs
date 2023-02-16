namespace Utilities.CustomModels
{
    public class CustomList<T>
        where T : class, new()
    {
        public CustomList(IQueryable<T> IQuery) { this.List = IQuery.ToList(); }
        public IEnumerable<T> List { get; }
        public PagedList Paged { set; get; }
    }
}
