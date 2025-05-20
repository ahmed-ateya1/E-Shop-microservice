using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            (string Details, string Title, int StatusCode) details = exception switch
            {
                InternalServerErrorException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError),

                NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status404NotFound),

                BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest),

                ValidationException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest),

                _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Details,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);


            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("errors", validationException);
            }

            context.Response.StatusCode = details.StatusCode;

            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
