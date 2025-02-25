using Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace ManagementSystem.Api.Infrastructure.Middlewares
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
                switch (error)
                {
                    case RateLimitExceededException:
                        await WriteError(context, HttpStatusCode.TooManyRequests, error.Message);
                        break;

                    default:
                        await WriteError(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
                        break;
                }
            }
        }

        static async Task WriteError(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";

            var errorResponse = new
            {
                error = message
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
