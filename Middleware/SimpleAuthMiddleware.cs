namespace UserManagementApi.Middleware
{
    public class SimpleAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Swagger endpointlerine izin ver (yoksa arayüzü göremezsin)
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            // Header kontrolü
            if (!context.Request.Headers.TryGetValue("Authorization", out var extractedToken))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Token eksik.");
                return;
            }

            // Basit Token Kontrolü (Şifremiz: "TechHiveSecret")
            if (extractedToken != "TechHiveSecret") 
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Gecersiz Token.");
                return;
            }

            await _next(context);
        }
    }
}