using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using CleanArchi.Domain;

namespace CleanArchi.Infrastructure.Filter
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is AppException)
            {
                HandleAppException(context, (AppException)exception);
            }
            else
            {
                HandleOtherException(context, exception);
            }
        }

        private void HandleAppException(ExceptionContext context, AppException appException)
        {
            context.Result = new ObjectResult(new { error = appException.Message })
            {
                StatusCode = 400,
                DeclaredType = typeof(string)
            };
            context.ExceptionHandled = true;

            _logger.LogError(appException, "AppException");
        }

        private void HandleOtherException(ExceptionContext context, Exception exception)
        {
            context.Result = new ObjectResult(new { error = "Internal Server Error" })
            {
                StatusCode = 500,
                DeclaredType = typeof(string)
            };
            context.ExceptionHandled = true;

            _logger.LogError(exception, "Unhandled Exception");
        }

        public ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            _logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);
            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled
            return ValueTask.FromResult(false);
        }
    }

}
