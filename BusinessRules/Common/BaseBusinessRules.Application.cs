using BusinessRules.Common.Interfaces;
using DataAccess.Common.Interfaces;
using Utilities.CustomModels;

namespace BusinessRules.Common
{
    public abstract partial class BaseBusinessRules<T, TImplementacion> : BaseBusinessRules, IRepositoryBase<T>, IBaseBusinessRules<T>
          where T : class, new()
          where TImplementacion : IRepositoryBase<T>
    {
        protected bool withAudit = true;
        protected bool withLogAudit = true;

        public void SetParameterBusinessRules(ParameterBusinessRules parameterBusinessRules)
        {
            DaoNegocio.SetParameterBusinessRules(parameterBusinessRules);
            EventParametersBusinessRules?.Invoke(this, parameterBusinessRules);
        }

        public event EventHandler EventParametersBusinessRules;
        
    }
}
