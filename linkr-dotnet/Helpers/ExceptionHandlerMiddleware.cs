using linkr_dotnet.Responses;
using System.Net;

namespace linkr_dotnet.Helpers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                await response.WriteAsJsonAsync<ResponseModel>(new(HttpStatusCode.InternalServerError, error.Message));
            }
        }
    }
}
