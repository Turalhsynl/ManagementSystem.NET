using Application;
using DAL.SqlServer;
using ManagementSystem.Api.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>();

var conn = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(conn);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RateLimitMiddleware>(2, TimeSpan.FromMinutes(1));
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
