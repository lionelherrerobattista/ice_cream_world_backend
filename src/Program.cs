using ice_cream_world_backend.models;
using IceCreamWorld.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
builder.Services.AddScoped<IFlavorRepository, FlavorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// middleware components:
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
