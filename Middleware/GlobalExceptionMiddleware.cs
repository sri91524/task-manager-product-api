using System.Net;
using System.Text.Json;
using TaskManager.DTOs;

namespace TaskManager.Middleware
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
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "applicaton/json";

            var response = new ErrorResponseDto
            {                
                StatusCode = (int)statusCode,
                Message = message,
                TimeStamp = DateTime.Now
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}
