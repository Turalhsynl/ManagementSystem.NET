using Common.Exceptions;
using System.Collections.Concurrent;

namespace ManagementSystem.Api.Infrastructure.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _requestLimit;
        private readonly TimeSpan _timeSpan;
        private readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new();
        private readonly IHttpContextAccessor _contextAccessor;

        public RateLimitMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeSpan, IHttpContextAccessor contextAccessor)
        {
            _next = next;
            _requestLimit = requestLimit;
            _timeSpan = timeSpan;
            _contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = _contextAccessor.HttpContext?.Connection.RemoteIpAddress!.ToString();
            var now = DateTime.UtcNow;
            var requestLog = _requestTimes.GetOrAdd(clientId, new List<DateTime>());

            lock (requestLog)
            {
                requestLog.RemoveAll(timeStamp => timeStamp <= now - _timeSpan);
                if (requestLog.Count >= _requestLimit)
                {
                    throw new RateLimitExceededException("Rate limit exceeded.");
                }
                requestLog.Add(now);
            }
            await _next(context);
        }
    }
}
