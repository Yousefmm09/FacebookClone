using FacebookClone.Core.Bases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace FacebookClone.Core.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An unhandled exception occurred: {Message}", error.Message);
                await HandleException(context, error);
            }
        }

        private Task HandleException(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new ErrorResponse { Succeeded = false };

            switch (error)
            {
                case UnauthorizedAccessException:
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    _logger.LogWarning("Unauthorized access attempt: {Message}", error.Message);
                    break;

                case ValidationException:
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    _logger.LogWarning("Validation failed: {Message}", error.Message);
                    break;

                case KeyNotFoundException:
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    _logger.LogWarning("Resource not found: {Message}", error.Message);
                    break;

                case DbUpdateException dbEx:
                    // Extract more meaningful message from DB exception
                    var dbMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                    responseModel.Message = "A database error occurred. Please check your data and try again.";
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogError(dbEx, "Database update exception: {Message}", dbMessage);
                    break;

                case ArgumentException argEx:
                    responseModel.Message = argEx.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogWarning("Invalid argument: {Message}", argEx.Message);
                    break;

                case InvalidOperationException:
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogWarning("Invalid operation: {Message}", error.Message);
                    break;

                default:
                    // For unexpected exceptions, don't expose internal details in production
                    responseModel.Message = "An unexpected error occurred. Please try again later.";
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(error, "Unhandled exception: {Message}", error.Message);
                    
                    // Include stack trace in development mode
                    if (context.RequestServices.GetService(typeof(IHostingEnvironment)) is IHostingEnvironment env && 
                        env.IsDevelopment())
                    {
                        responseModel.Message = error.Message;
                        responseModel.Data = error.StackTrace;
                    }
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var result = JsonSerializer.Serialize(responseModel, options);
            return response.WriteAsync(result);
        }
    }
}
