using DataAccess.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace DOUBLEV.Tickets.API.Controllers.Base
{
    [Route(Constants.DefaultUriWebApi + "[controller]")]
    [Produces("application/json")]
    [ApiController]
    public partial class BaseController<T, TImplementacion> : BaseUtilities
        where T : class, new()
        where TImplementacion : IRepositoryBase<T>, BusinessRules.Common.Interfaces.IBaseBusinessRules<T>
    {
        protected readonly TImplementacion RepoBusinessRules;

        public BaseController(TImplementacion repoBusinessRules)
        {
            this.RepoBusinessRules = repoBusinessRules;
        }

        #region Metodos Tranversales de la APIs

        /// <summary>
        /// Retorna Todos los Registros de la Entidad
        /// </summary>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Lista de Registros de la Entidad <typeparamref name="T"/>
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <typeparamref name="T"/>
        /// </remarks>
        /// <response code="200">Retorna la Lista de Registros de la Entidad <typeparamref name="T"/></response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> GetAsync()
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                return ResultApi(await RepoBusinessRules.ToListAsync());
            });
        }

        /// <summary>
        /// Crear Registro Nuevo de la entidad
        /// </summary>
        /// <param name="objBase">Objeto A Crear</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <typeparamref name="T"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> PostAsync([FromBody] T objBase)
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                var objParam = GetParameters(objBase);
                return ResultApi(((await RepoBusinessRules.CreateAsync(objBase) ?? 0) > 0), CusMessageCreate, objParam);
            });
        }

        /// <summary>
        /// Actualizar Registro de la entidad
        /// </summary>
        /// <param name="objBase">Objeto a Modificar</param>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Respuesta de la operacion y el Objeto de ResponseApi
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <remarks>
        /// Entidad <typeparamref name="T"/>
        /// </remarks>
        /// <response code="200">Retorna Respuesta de la operacion y el Objeto de ResponseApi</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> PutAsync([FromBody] T objBase)
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                var objParam = GetParameters(objBase);
                return ResultApi((await RepoBusinessRules.EditAsync(objBase)) ?? false, CusMessageUpdate, objParam);
            });
        }

        #endregion

        #region Metodos Genericos para las APIs Buscar/Listar/Eliminar

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">Expresión para las busquedas avanzada de registros</param>
        /// <param name="orderBy">Columna por la que se ordenara</param>
        /// <param name="direcOrder">Dirección (Asc, Desc)</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con el registro encontrado por Id(s) de la Entidad
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected Task<IActionResult> GetListAsync(Expression<Func<T, bool>> expression, string orderBy, string direcOrder)
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                return ResultApi(await RepoBusinessRules.ToListAsync(new ParameterOfList<T>(expression, orderBy, direcOrder)).ConfigureAwait(false));
            });
        }

        /// <summary>
        /// Retorna el Registro Por Id Entontrado de tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Expresión para las busquedas avanzada de registros</param>
        /// <param name="ids">Codigos Revisividos para busqueda</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con el registro encontrado por Id(s) de la Entidad
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected Task<IActionResult> GetGenericAsync(Expression<Func<T, bool>> expression, params string[] ids)
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                var objBase = await RepoBusinessRules.SearchAsync(expression);
                if (objBase.IsNotNull())
                {
                    return ResultApi(objBase);
                }

                return ResultApi(StatusCodes.Status404NotFound, EnumerationException.Message.ErrNoEncontrado, ids);
            });
        }

        /// <summary>
        /// Eliminar Registro de tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Expresión para las eliminaciones avanzada de registros</param>
        /// <param name="ids">Codigos Revisividos para eliminación</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la respuesta de la operacion y el Objeto de ResponseApi
        ///              Status404NotFound Si no se encuentra un regsitro por Id(s) de la Entidad
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected Task<IActionResult> DeleteGenericAsync(Expression<Func<T, bool>> expression, params string[] ids)
        {
            return ExceptionBehaviorAsync(async () =>
            {
                ValidateAuthorizationPermissions();

                var objBase = RepoBusinessRules.Search(expression);
                if (objBase.IsNotNull())
                {
                    GetParameters(objBase);
                    return ResultApi((await RepoBusinessRules.DeleteAsync(objBase)) ?? false, CusMessageDelete, ids);
                }

                return ResultApi(StatusCodes.Status404NotFound, EnumerationException.Message.ErrNoEncontrado, ids);
            });
        }

        /// <summary>
        /// Retorna Todos los Registros Paginados segun el 
        ///     numero de registros, Ordenados y en la dirección Configurada de tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="page">Número de Pagina</param>
        /// <param name="numberRecords">Numero de Registros Por Pagina a Mostrar</param>
        /// <param name="orderBy">Columna por la que se ordenara</param>
        /// <param name="direcOrder">Dirección (Asc, Desc)</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la Lista de Registros de la Entidad y Un objeto con los datos de la paginación
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected IActionResult GetListOrderPaged(int page, int numberRecords, string orderBy, string direcOrder)
        {
            return GetListOrderPaged(page, numberRecords, orderBy, direcOrder, null);
        }

        /// <summary>
        /// Retorna Todos los Registros Paginados segun el 
        ///     numero de registros, Ordenados y en la dirección Configurada de tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="page">Número de Pagina</param>
        /// <param name="numberRecords">Numero de Registros Por Pagina a Mostrar</param>
        /// <param name="orderBy">Columna por la que se ordenara</param>
        /// <param name="direcOrder">Dirección (Asc, Desc)</param>
        /// <param name="objFilter">Objeto con los valores filtro</param>
        /// <returns>
        /// Retorna en el response de la petición
        ///              Status200OK Con la Lista de Registros de la Entidad y Un objeto con los datos de la paginación
        ///              Status400BadRequest Sí ocurrió una falla, validación o error controlado
        ///              Status500InternalServerError Sí ocurrió una falla o error NO controlado
        ///              Status403Forbidden Sí no tiene permisos para ejecutar la acción
        /// </returns>
        protected IActionResult GetListOrderPaged(int page, int numberRecords, string orderBy, string direcOrder, Filter objFilter)
        {
            return ExceptionBehavior(() =>
            {
                ValidateAuthorizationPermissions();

                return ((objFilter.IsNotNull() && objFilter.Filters.IsNotNull())
                    ?
                    ResultApi(RepoBusinessRules.ToListPaged(new ParameterOfList<T>(page, numberRecords, orderBy, direcOrder, objFilter)))
                    :
                    ResultApi(RepoBusinessRules.ToListPaged(new ParameterOfList<T>(page, numberRecords, orderBy, direcOrder))));
            });
        }

        #endregion
    }
}
