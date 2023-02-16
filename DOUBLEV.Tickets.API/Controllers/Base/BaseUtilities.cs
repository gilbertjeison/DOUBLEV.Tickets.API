using Microsoft.AspNetCore.Mvc;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace DOUBLEV.Tickets.API.Controllers.Base
{
    public class BaseUtilities : ControllerBase
    {
        protected IActionResult ExceptionResultApi(CustomException customException)
        {
            if (customException.IsNotNull())
            {
                return ResultResponseApi(StatusCodeRequest(customException.TypeException),
                                         customException.ErrorBusiness,
                                         customException.TagTextBusiness);
            }

            return ResultResponseApi(StatusCodes.Status500InternalServerError, EnumerationMessage.Message.ErrorGeneral, null);
        }

        private int StatusCodeRequest(EnumerationException.TypeCustomException typeCustomException)
        {
            return typeCustomException switch
            {
                EnumerationException.TypeCustomException.Validation => StatusCodes.Status400BadRequest,
                EnumerationException.TypeCustomException.BusinessException => StatusCodes.Status500InternalServerError,
                EnumerationException.TypeCustomException.NoContent => StatusCodes.Status204NoContent,
                EnumerationException.TypeCustomException.Unauthorized => StatusCodes.Status403Forbidden,
                EnumerationException.TypeCustomException.Undefined => StatusCodes.Status406NotAcceptable,
                _ => StatusCodes.Status500InternalServerError,
            };
        }

        protected IActionResult ResultApi(int statusCodes, EnumerationException.Message message, params string[] tags)
        {
            return ResultResponseApi(statusCodes, message, tags);
        }

        protected IActionResult ResultApi(int statusCodes, EnumerationException.Message message)
        {
            return ResultApi(statusCodes, message, null);
        }

        protected IActionResult ResultApi(bool statusCodes, EnumerationException.Message message, params string[] tags)
        {
            return ResultResponseApi((statusCodes) ?
                                        StatusCodes.Status200OK :
                                        StatusCodes.Status400BadRequest, message, tags);
        }

        protected IActionResult ResultApi(bool statusCodes, EnumerationException.Message message)
        {
            return ResultApi(statusCodes, message, null);
        }

        protected IActionResult ResultApi<T>(T objResult)
        { return StatusCode(StatusCodes.Status200OK, objResult); }

        private IActionResult ResultResponseApi(int statusCodes, EnumerationException.Message message, string[] tags)
        {
            return StatusCode(statusCodes, new ResponseApi(message, tags, GetCategoryMessage(statusCodes)));
        }

        private static EnumerationApplication.CategoryMessage GetCategoryMessage(int statusCodes)
        {
            return statusCodes switch
            {
                StatusCodes.Status200OK => EnumerationApplication.CategoryMessage.Success,
                StatusCodes.Status500InternalServerError => EnumerationApplication.CategoryMessage.Error,
                StatusCodes.Status400BadRequest => EnumerationApplication.CategoryMessage.Warning,
                _ => EnumerationApplication.CategoryMessage.Alert
            };
        }

        protected IActionResult ExceptionBehavior(Func<IActionResult> fnCallBack)
        {
            try
            {
                return fnCallBack();
            }
            catch (CustomException ex) { return ExceptionResultApi(ex); }
        }

        protected async Task<IActionResult> ExceptionBehaviorAsync(Func<Task<IActionResult>> fnCallBack)
        {
            try
            {
                return await fnCallBack().ConfigureAwait(false);
            }
            catch (CustomException ex) { return ExceptionResultApi(ex); }
        }
    }
}
