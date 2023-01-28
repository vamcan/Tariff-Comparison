using Microsoft.AspNetCore.Mvc;
using TariffComparison.Web.Api.Base.ApiResult;

namespace TariffComparison.Web.Api.Base
{
    public class ServerErrorResult : IActionResult
    {
        public string Message { get; }

        public ServerErrorResult(string message)
        {
            Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ApiResult.ApiResult(false, ApiResultStatusCode.ServerError, Message);
            await context.HttpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
