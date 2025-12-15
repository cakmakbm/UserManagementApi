namespace UserManagementApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. İstek Bilgilerini Logla
            _logger.LogInformation($"Gelen İstek: {context.Request.Method} {context.Request.Path}");

            // Bir sonraki middleware'e geç
            await _next(context);

            // 2. Yanıt Bilgilerini Logla (Dönüşte)
            _logger.LogInformation($"Yanıt Durumu: {context.Response.StatusCode}");
        }
    }
}