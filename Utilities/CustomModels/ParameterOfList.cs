using System.Linq.Expressions;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace Utilities.CustomModels
{
    public class ParameterOfList<T>
        where T : class, new()
    {
        private int Page = -1;
        public int Take { set; get; }
        public long MaxCount { set; private get; }
        public int Skip => (Page > 0 && Take > 0) ? (Take * (Page - 1)) : Page;
        public long TotalPages => (MaxCount > 0 && Take > 0) ? (int)Math.Ceiling(MaxCount / (double)Take) : 0;
        private long RecordsFrom => ((RecordsTo > 0 && Take > 0) ? (RecordsTo - Take) : 0);
        private long RecordsTo => ((Take > 0 && Page > 0) ? (Take * Page) : 0);
        public Filter WhereDynamic { private set; get; }
        public Expression<Func<T, bool>> Filter { private set; get; }
        public Expression<Func<T, object>>[] Include { private set; get; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { private set; get; }
        public Tuple<string, EnumerationApplication.Orden> OrderByDynamic { private set; get; }

        public PagedList TextPag => new PagedList
        {
            Page = Page,
            PageSize = Take,
            TotalPages = TotalPages,
            MaxCount = MaxCount,
            RecordsFrom = RecordsFrom + 1,
            RecordsTo = (RecordsTo > MaxCount) ? MaxCount : RecordsTo
        };

        public ParameterOfList(Expression<Func<T, bool>> expression)
        {
            Clear();
            this.Filter = expression;
        }


        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden, Filter whereDynamic)
            : this(page, expression, orderByDynamic, DirecOrden)
        {
            this.Take = pageSize;
            WhereDynamic = whereDynamic;
        }

        public ParameterOfList(Expression<Func<T, bool>> expression,
                                    params Expression<Func<T, object>>[] include) : this(expression)
        {
            Include = include;
        }

        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBY,
                                    params Expression<Func<T, object>>[] include) : this(page, pageSize, expression, orderBY)
        {
            Include = include;
        }

        public ParameterOfList(Expression<Func<T, bool>> expression,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression) { this.OrderBy = orderBY; }

        public ParameterOfList(Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden) : this(expression)
        {
            if (DirecOrden.IsValid())
            {
                DirecOrden = DirecOrden.FirstCharToUpper();
            }

            if (!Enum.TryParse(DirecOrden, out EnumerationApplication.Orden eOrden))
            {
                eOrden = EnumerationApplication.Orden.Asc;
            }

            OrderByDynamic = new Tuple<string, EnumerationApplication.Orden>(orderByDynamic, eOrden);
        }

        public ParameterOfList(int page, Expression<Func<T, bool>> expression) : this(page, expression, null) { }

        public ParameterOfList(int page, Expression<Func<T, bool>> expression,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression, orderBY) { this.Page = page; }

        public ParameterOfList(int page, int pageSize,
                                Expression<Func<T, bool>> expression) : this(page, pageSize, expression, null)
        { }

        public ParameterOfList(int page, int pageSize,
                                Expression<Func<T, bool>> expression,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, expression, orderBY)
        {
            this.Take = pageSize;
        }

        public ParameterOfList(Expression<Func<T, bool>> expression, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression, orderBY) { this.Take = pageSize; }

        public ParameterOfList(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, pageSize, null, orderBY) { }

        public ParameterOfList(int page, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, null, orderBY) { }

        public ParameterOfList(int page, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden) : this(page, expression, null)
        {
            if (DirecOrden.IsValid())
            {
                DirecOrden = DirecOrden.FirstCharToUpper();
            }

            if (!Enum.TryParse(DirecOrden, out EnumerationApplication.Orden eOrden))
            {
                eOrden = EnumerationApplication.Orden.Asc;
            }

            OrderByDynamic = new Tuple<string, EnumerationApplication.Orden>(orderByDynamic, eOrden);
        }

        public ParameterOfList(int page, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden,
                                    params Expression<Func<T, object>>[] include) : this(page, expression, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        public ParameterOfList(int page, int pageSize, string orderByDynamic, string DirecOrden) : this(page, null, orderByDynamic, DirecOrden) { this.Take = pageSize; }

        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden)
            : this(page, expression, orderByDynamic, DirecOrden)
        { this.Take = pageSize; }

        public ParameterOfList(int page, string orderByDynamic, string DirecOrden) : this(page, null, orderByDynamic, DirecOrden) { }

        public ParameterOfList(int page, string orderByDynamic, string DirecOrden, params Expression<Func<T, object>>[] include)
            : this(page, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        public ParameterOfList(int page, int pageSize, string orderByDynamic, string DirecOrden, Filter whereDynamic) :
            this(page, null, orderByDynamic, DirecOrden)
        {
            this.Take = pageSize;
            WhereDynamic = whereDynamic;
        }

        public ParameterOfList(Filter whereDynamic)
        {
            Clear();
            WhereDynamic = whereDynamic;
        }

        public ParameterOfList(int pageSize)
        {
            Clear();
            this.Take = pageSize;
        }

        public ParameterOfList(params Expression<Func<T, object>>[] include)
        {
            Clear();
            this.Include = include;
        }

        #region Configurar ParameterOfList

        public void Clear()
        {
            MaxCount = 0;
            Page = -1;
            Take = -1;
            Filter = null;
            OrderBy = null;
            OrderByDynamic = new Tuple<string, EnumerationApplication.Orden>(string.Empty, EnumerationApplication.Orden.Asc);
            WhereDynamic = null;
            Include = null;
        }

        public void Add(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }

        public void Add(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            Filter = filter;
            OrderBy = orderBy;
        }

        #endregion
    }
}
