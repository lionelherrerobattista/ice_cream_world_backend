using ice_cream_world_backend.models;
using IceCreamWorld.Data;
using IceCreamWorld.Middleware;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// cors
var allowedOrigins = Environment.GetEnvironmentVariable("PRODUCTION_URL")
    ?? builder.Configuration.GetSection("AllowedOrigins").Get<string>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// database
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IceCreamWorldContext>(options =>
    // options.UseSqlite(builder.Configuration.GetConnectionString("IceCreamWorldContext"))
    options.UseNpgsql(connectionString)
);
// rate limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("default", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueLimit = 2;
    });
});
builder.Services.AddScoped<IFlavorRepository, FlavorRepository>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// middleware components:
app.UseCors();
app.UseRateLimiter();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self';");
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
