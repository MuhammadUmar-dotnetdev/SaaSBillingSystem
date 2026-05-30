using SaaSBillingSystem.Shared.Exceptions;
using System.Net;
using System.Text.Json;

namespace SaaSBillingSystem.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case NotFoundException:
                    message = exception.Message;
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    message = exception.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                default:
                    message = exception.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new
            {
                success = false,
                message = message,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
