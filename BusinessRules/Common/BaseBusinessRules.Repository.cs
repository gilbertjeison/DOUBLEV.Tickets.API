using BusinessRules.Common.Interfaces;
using DataAccess.Common.Interfaces;
using System.Linq.Expressions;
using System.Transactions;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace BusinessRules.Common
{
    public abstract partial class BaseBusinessRules<T, TImplementacion> : BaseBusinessRules, IRepositoryBase<T>, IBaseBusinessRules<T>
          where T : class, new()
          where TImplementacion : IRepositoryBase<T>
    {
        protected readonly TImplementacion DaoNegocio;

        public IMainContext RepositoryContext => DaoNegocio.RepositoryContext;

        public void SetAutoSave(bool value) { this.DaoNegocio.SetAutoSave(value); }

        protected BaseBusinessRules(TImplementacion daoNegocio) : base(daoNegocio?.GetType()?.Name ?? string.Empty)
        {
            DaoNegocio = daoNegocio;
        }

        public T Search(Expression<Func<T, bool>> expression)
        {
            return ExceptionBehavior(() => DaoNegocio.Search(expression));
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            return ExceptionBehavior(() => DaoNegocio.Count(expression));
        }

        public CustomList<T> ToListPaged()
        {
            return ToListPaged(null);
        }

        public CustomList<T> ToListPaged(ParameterOfList<T> parameterOfList)
        {
            return ExceptionBehavior(() => DaoNegocio.ToListPaged(parameterOfList));
        }

        #region Metodos Asincronos

        public Task<List<T>> ToListAsync()
        {
            return ToListAsync(null);
        }

        public Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList)
        {
            return ExceptionBehavior(() => DaoNegocio.ToListAsync(parameterOfList));
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await ExceptionBehaviorAsync(async () => await DaoNegocio.SearchAsync(expression).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return await ExceptionBehaviorAsync(async () => await DaoNegocio.SearchAsync(expression, includes).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task<int?> CreateAsync(T objCreate)
        {
            if (objCreate.IsNotNull())
            {
                var objReturn = await ExceptionBehaviorAsync(async () =>
                {
                    ValidationsToCreate(objCreate);

                    using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var objReturn = await DaoNegocio.CreateAsync(objCreate).ConfigureAwait(false);

                    tran.Complete();
                    return objReturn;
                }).ConfigureAwait(false);

                return objReturn;
            }
            else
            {
                throw new CustomException(EnumerationException.TypeCustomException.Validation,
                    EnumerationException.Message.ObjectEmpty);
            }
        }

        public async Task<int?> CreateAsync(IEnumerable<T> objCreate)
        {
            return await ExceptionBehaviorAsync(() => DaoNegocio.CreateAsync(objCreate)).ConfigureAwait(false);
        }

        public async Task<bool?> EditAsync(T objEdit)
        {
            if (objEdit.IsNotNull())
            {
                var objReturn = await ExceptionBehaviorAsync(async () =>
                {
                    ValidationsToEdit(objEdit);

                    using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);                    
                    var objReturn = await DaoNegocio.EditAsync(objEdit).ConfigureAwait(false);

                    tran.Complete();
                    return objReturn;
                }).ConfigureAwait(false);

                return objReturn;
            }
            else
            {
                throw new CustomException(EnumerationException.TypeCustomException.Validation,
                    EnumerationException.Message.ObjectEmpty);
            }
        }

        public async Task<bool?> EditAsync(IEnumerable<T> objEdit)
        {
            return await ExceptionBehaviorAsync(() => DaoNegocio.EditAsync(objEdit)).ConfigureAwait(false);
        }

        public async Task<bool?> DeleteAsync(T objDelete)
        {
            if (objDelete.IsNotNull())
            {
                var objReturn = await ExceptionBehaviorAsync(async () =>
                {
                    ValidationsToDelete(objDelete);

                    using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var objReturn = await DaoNegocio.DeleteAsync(objDelete).ConfigureAwait(false);
                    tran.Complete();
                    return objReturn;
                }).ConfigureAwait(false);

                return objReturn;
            }
            else
            {
                throw new CustomException(EnumerationException.TypeCustomException.Validation,
                    EnumerationException.Message.ObjectEmpty);
            }
        }

        public async Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            return await ExceptionBehaviorAsync(async () =>
            {
                var objDelete = Search(expression);
                if (objDelete.IsNotNull())
                {
                    ValidationsToDelete(objDelete);

                    using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var objReturn = await DaoNegocio.DeleteAsync(objDelete).ConfigureAwait(false);
                    tran.Complete();
                    return objReturn;
                }
                else
                {
                    throw new CustomException(EnumerationException.TypeCustomException.Validation,
                        EnumerationException.Message.ErrNoEncontrado);
                }
            }).ConfigureAwait(false);
        }

        public async Task<bool?> DeleteRangeAsync(Expression<Func<T, bool>> expression)
        {
            return await ExceptionBehaviorAsync(() => DaoNegocio.DeleteRangeAsync(expression)).ConfigureAwait(false);
        }

        public async Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete)
        {
            return await ExceptionBehaviorAsync(() => DaoNegocio.DeleteRangeAsync(objDelete)).ConfigureAwait(false);
        }

        #endregion

        #region Metodo expuesto para Validaciones Crear, Editar y Eliminar


        protected virtual void ValidationsToCreate(T entity) { entity.TrimAll(); }

        protected virtual void ValidationsToEdit(T entity) { entity.TrimAll(); }

        protected virtual void ValidationsToDelete(T entity) { }

        internal void ValidationsExpression(Expression<Func<T, bool>> expression, EnumerationMessage.Message messageValidation)
        {
            this.ValidationsExpression(expression, messageValidation, null);
        }

        internal void ValidationsExpression(Expression<Func<T, bool>> expression, EnumerationMessage.Message messageValidation, string[] tagTextBusiness)
        {
            if (Count(expression) > 0)
            {
                throw new CustomException(EnumerationException.TypeCustomException.Validation, messageValidation, tagTextBusiness);
            }
        }

        #endregion
    }
}
