using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var requestTime = _stopwatch.ElapsedMilliseconds;
            if (requestTime / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {requestTime} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
