using Newtonsoft.Json;
using System.Net;
using WebAtb.Exceptions;

namespace WebAtb.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case AppException appException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
                //case NotFoundException _:
                //    code = HttpStatusCode.NotFound;
                //    break;
                default:
                    result = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
            }
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
