using irede.core.Interfaces.Base;
using irede.shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
namespace irede.api.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {

        private IServiceBase _serviceBase;

        [NonAction]
        protected async Task<IActionResult> ResponseAsync(object result, IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;

            if (!serviceBase.Notifications.Any())
            {
                try
                {
                    return await CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    var message = new
                    {
                        errors = $"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista.",
                        exception = ex.ToString()
                    };
                    // Aqui devo logar o erro
                    return await CreateResponse(HttpStatusCode.Conflict, message);
                }
            }
            else
            {
                return await CreateResponse(HttpStatusCode.BadRequest, serviceBase.Notifications);
            }
        }

        [NonAction]
        protected async Task<IActionResult> ResponseExceptionAsync(Exception ex)
        {
            return await CreateResponse(HttpStatusCode.InternalServerError, new { errors = ex.Message, exception = ex.ToString() });
        }
        private async Task<IActionResult> CreateResponse(HttpStatusCode statusCode, object value)
        {
            const string jsonContentType = "application/json";

            return StatusCode((int)statusCode, value.AsJson());

            //return StatusCode((int)statusCode, new StringContent(value.AsJson(), Encoding.UTF8, jsonContentType));
            //return new ActionResult
            //{
            //    Content = new StringContent(value.AsJson(), Encoding.UTF8, jsonContentType),
            //    StatusCode = statusCode
            //};
        }
    }
}
