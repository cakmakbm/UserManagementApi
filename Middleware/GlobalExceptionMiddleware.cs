using System.Net;
using System.Text.Json;

namespace UserManagementApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // İşlemi yapmayı dene
                await _next(context);
            }
            catch (Exception ex)
            {
                // Hata olursa yakala ve logla
                _logger.LogError($"Beklenmedik Hata: {ex.Message}");
                
                // Kullanıcıya JSON dön
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new { error = "Internal server error.", detail = exception.Message };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}