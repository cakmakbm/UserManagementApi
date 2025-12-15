using UserManagementApi.Middleware;
using UserManagementApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen; // Gerekirse ekle

var builder = WebApplication.CreateBuilder(args);

// 1. Servisler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

var app = builder.Build();

// 2. Middleware Pipeline (Sıralama Önemli!)

// A. Hata Yakalama (En dışta, her şeyi kapsar)
app.UseMiddleware<GlobalExceptionMiddleware>();

// B. Swagger (Geliştirici dostu arayüz)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1");
});

// C. Kimlik Doğrulama (Auth)
app.UseMiddleware<SimpleAuthMiddleware>();

// D. Loglama (Sadece yetkili istekleri loglar, Auth'dan sonra geldiği için)
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();