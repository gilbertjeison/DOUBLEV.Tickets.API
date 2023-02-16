using Utilities.CustomModels;
using Utilities.Enumerations;

namespace BusinessRules.Common
{
    public abstract partial class BaseBusinessRules
    {
        public string NameClassReference { get; }


        /// <summary>
        /// Constructor de la Clase <BaseBusinessRules></BaseBusinessRules>
        /// </summary>
        protected BaseBusinessRules() { NameClassReference = string.Empty; }

        /// <summary>
        /// Constructor de la Clase <BaseBusinessRules></BaseBusinessRules>
        /// </summary>
        protected BaseBusinessRules(string classBussiness) { NameClassReference = classBussiness; }

        #region Captura de Excepciones

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="fnAccion"></param>
        /// <returns></returns>
        protected TReturn ExceptionBehavior<TReturn>(Func<TReturn> fnAccion)
        {
            TReturn returnBehavior;

            try
            {
                returnBehavior = fnAccion();
            }
            catch (Exception ex)
            {
                returnBehavior = default;
                HandleExceptions(ex);
            }

            return returnBehavior;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="fnAccion"></param>
        /// <returns></returns>
        protected async Task<TReturn> ExceptionBehaviorAsync<TReturn>(Func<Task<TReturn>> fnAccion)
        {
            TReturn returnBehavior;

            try
            {
                returnBehavior = await fnAccion().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                returnBehavior = default;
                HandleExceptions(ex);
            }

            return returnBehavior;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        protected void HandleExceptions(Exception ex)
        {
            //TODO: Implementar servicio de auditoría
            //this.CreateLogAudit(ex);

            if (ex.GetType() == typeof(CustomException) &&
                (((CustomException)ex).TypeException == EnumerationException.TypeCustomException.Validation))
            {
                throw ex;
            }

            ExceptionProcessorHelper.HandleException(ex);
        }

        #endregion
    }
}
