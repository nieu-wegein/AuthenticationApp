using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AuthenticationApp.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
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
                var problemDetails = new ProblemDetails
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                    Title = "Internal server error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string json = JsonSerializer.Serialize(problemDetails);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
