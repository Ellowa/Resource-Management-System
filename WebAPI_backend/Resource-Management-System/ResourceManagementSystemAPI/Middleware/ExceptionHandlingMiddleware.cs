using BysinessServices.Models;
using System.Net;

namespace ResourceManagementSystemAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestNext;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestNext, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestNext = requestNext;
            _logger = logger;
        }

        private async Task HandleAsync(HttpContext context, HttpStatusCode statusCode, string exMessage)
        {
            _logger.LogError(exMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorMessage errorMessage = new()
            {
                Message = exMessage,
                StatusCode = (int)statusCode
            };

            await response.WriteAsJsonAsync(errorMessage);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestNext(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
