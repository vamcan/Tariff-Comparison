using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Core.Application.Base.Common;
using TariffComparison.Web.Api.Base.Utility;

namespace TariffComparison.Web.Api.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
     
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        protected IActionResult OperationResult(dynamic result)
        {
            if (result is null)
                return new ServerErrorResult("Server Error");

            if (!((object)result).IsAssignableFromBaseTypeGeneric(typeof(OperationResult<>)))
            {
                throw new InvalidCastException("Given Type is not an OperationResult<T>");
            }


            if (result.IsSuccess) return result.Result is bool ? Ok() : Ok(result.Result);

            if (result.IsNotFound)
            {

                ModelState.AddModelError("GeneralError", result.ErrorMessage);

                var notFoundErrors = new ValidationProblemDetails(ModelState);

                return NotFound(notFoundErrors.Errors);
            }

            ModelState.AddModelError("GeneralError", result.ErrorMessage);

            var badRequestErrors = new ValidationProblemDetails(ModelState);

            return BadRequest(badRequestErrors.Errors);

        }
    }
}
