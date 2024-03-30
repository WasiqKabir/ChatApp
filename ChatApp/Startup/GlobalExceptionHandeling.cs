using BusinessEntities.Exceptions;
using System;
using System.Net;
using System.Text.Json;

namespace ChatApp.Startup
{
    public class GlobalExceptionHandeling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandeling(RequestDelegate next)
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message;

            if (ex is BusinessException)
            {
                message = ex.Message;
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            }
            else
            {
                message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }


            // Create custom error response
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            // Serialize response object to JSON
            var json = JsonSerializer.Serialize(response);

            // Write JSON response to the response body
            await context.Response.WriteAsync(json);
        }
    }
}
