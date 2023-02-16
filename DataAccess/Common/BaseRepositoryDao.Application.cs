using DataAccess.Common.Interfaces;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExpressionHelper;
using Utilities.ExtensionMethods;

namespace DataAccess.Common
{
    public abstract partial class BaseRepositoryDao<T>
          where T : class, new()
    {
        public IMainContext RepositoryContext { get; protected set; }

        protected bool autoSave = true;

        protected IQueryable<T> WheresTransversal(IQueryable<T> IQuery)
        {
            if (this.ParameterBusinessRules.IsNotNull())
            {
                string isValid = GetProperty(new T(), "CompanyId");
                if (isValid.IsValid())
                {
                    return IQuery.Where(ExpressionHelper.GetCriteriaWhere<T>(isValid,
                                                                EnumerationApplication.OperationExpression.Equals,
                                                                this.ParameterBusinessRules.CompanyId));
                }
            }
            return IQuery;
        }

        private ParameterBusinessRules ParameterBusinessRules { set; get; }

        public void SetParameterBusinessRules(ParameterBusinessRules parameterBusinessRules)
        {
            ParameterBusinessRules = parameterBusinessRules;
        }

        private string? GetProperty(T entity, string name)
        {
            return RepositoryContext.Model.FindEntityType(entity.GetType().FullName.ToLower().Contains("prox") ?
                                                            entity.GetType().BaseType.FullName :
                                                            entity.GetType().FullName)?.FindProperty(name)?.Name;
        }
    }
}
