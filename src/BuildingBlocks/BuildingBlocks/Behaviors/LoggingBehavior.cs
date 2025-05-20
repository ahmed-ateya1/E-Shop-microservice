using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull , IRequest<TResponse> 
        where TResponse : notnull
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling request: {RequestType} - {@Request}", typeof(TRequest).Name, request);

            var response = next();

            logger.LogInformation("Handled request: {RequestType} - {@Response}", typeof(TRequest).Name, response);

            return response;
        }
    }
}
