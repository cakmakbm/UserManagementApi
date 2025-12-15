using UserManagementApi.Middleware;
using UserManagementApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

var app = builder.Build();




app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1");
});


app.UseMiddleware<SimpleAuthMiddleware>();


app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
