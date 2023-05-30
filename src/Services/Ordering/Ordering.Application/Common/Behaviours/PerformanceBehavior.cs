using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ordering.Application.Common.Behaviours
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMiliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMiliseconds <= 500) return response;
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Application long running request: {Name} ({ElapsedMiliseconds} miliseconds {@Request}",
                requestName, elapsedMiliseconds, request);

            return response;
        }
    }
}