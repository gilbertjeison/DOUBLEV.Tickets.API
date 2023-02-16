using Microsoft.EntityFrameworkCore;
using Utilities.ExtensionMethods;

namespace DataAccess.Common
{
    public abstract partial class BaseRepositoryDao<T>
          where T : class, new()
    {
        protected BaseRepositoryDao() { }

        public void SetAutoSave(bool value)
        {
            this.autoSave = value;
        }

        protected void EntityStateDetached(T obj)
        {
            if (obj.IsNotNull() && RepositoryContext.Entry(obj).IsNotNull())
            {
                RepositoryContext.Entry(obj).State = EntityState.Detached;
            }
        }

        protected void EntityStateModified(T objEdit)
        {
            if (RepositoryContext.Entry(objEdit).IsNotNull())
            {
                RepositoryContext.Entry(objEdit).State = EntityState.Modified;
            }
        }

        protected int RepositoryContextSaveChanges()
        {
            return autoSave ? RepositoryContext.SaveChanges() : 0;
        }

        protected async Task<bool> ReturnRepositoryContextSaveChangesAsync()
        {
            return autoSave && (await RepositoryContextSaveChangesAsync().ConfigureAwait(false) != 0);
        }

        protected async Task<int> RepositoryContextSaveChangesAsync()
        {
            return autoSave ? (await RepositoryContext.SaveChangesAsync().ConfigureAwait(false)) : 0;
        }
    }
}
