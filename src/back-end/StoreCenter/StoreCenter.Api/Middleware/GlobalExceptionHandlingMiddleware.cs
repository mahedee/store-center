using StoreCenter.Application.Common.Exceptions;
using StoreCenter.Domain.Exceptions;
using StoreCenter.Api.Models;
using System.Net;
using System.Text.Json;

namespace StoreCenter.Api.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context.TraceIdentifier;
            
            _logger.LogError(exception, "An error occurred. TraceId: {TraceId}", traceId);

            var errorResponse = exception switch
            {
                ValidationException validationEx => new ErrorResponse
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = "One or more validation errors occurred",
                    Instance = context.Request.Path,
                    TraceId = traceId,
                    Errors = validationEx.Errors
                },

                NotFoundException notFoundEx => new ErrorResponse
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "Resource Not Found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = notFoundEx.Message,
                    Instance = context.Request.Path,
                    TraceId = traceId
                },

                DomainException domainEx => new ErrorResponse
                {
                    Type = "https://example.com/errors/domain",
                    Title = "Domain Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = domainEx.Message,
                    Instance = context.Request.Path,
                    TraceId = traceId
                },

                _ => new ErrorResponse
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = "An unexpected error occurred",
                    Instance = context.Request.Path,
                    TraceId = traceId
                }
            };

            context.Response.StatusCode = errorResponse.Status;
            context.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}