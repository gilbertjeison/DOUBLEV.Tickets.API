using DataAccess.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace DOUBLEV.Tickets.API.Controllers.Base
{
    public partial class BaseController<T, TImplementacion>
        where T : class, new()
        where TImplementacion : IRepositoryBase<T>, BusinessRules.Common.Interfaces.IBaseBusinessRules<T>
    {
        protected EnumerationException.Message CusMessageCreate = EnumerationException.Message.MsjCreacion;

        protected EnumerationException.Message CusMessageUpdate = EnumerationException.Message.MsjActualizacion;

        protected EnumerationException.Message CusMessageDelete = EnumerationException.Message.MsjEliminacion;

        protected readonly IHttpContextAccessor ContextAccessor;

        private readonly string ShortNameCompany;

        public BaseController(TImplementacion repoBusinessRules, IHttpContextAccessor contextAccessor, IAuthorizationService AuthorizationService) : this(repoBusinessRules)
        {
            this.ContextAccessor = contextAccessor;
            RepoBusinessRules.SetParameterBusinessRules(new ParameterBusinessRules(Convert.ToInt32(this.ContextAccessor.HttpContext.Request.Headers["CompanyId"])));
            ShortNameCompany = this.ContextAccessor.HttpContext.Request.Headers["ShortNameCompany"];
        }

        [HttpGet("GetDisplayAttribute")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        public IActionResult GetDisplayAttribute()
        {
            return ExceptionBehavior(() => ResultApi(ExtensionMethods.GetAttributes<T>()));
        }

        protected void ValidateAuthorizationPermissions()
        {
            this.ValidateAuthorizationPermissions(null);
        }

        private void ValidateAuthorizationPermissions(string nameValidation)
        {
            //TODO: Implementar servicios de autorización
        }

        protected virtual string[] GetParameters(T objEntidad) => default;
    }
}
