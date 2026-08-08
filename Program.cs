using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tpo_DotNet_bb.Api.Api.Entities;
using Tpo_DotNet_bb.Api.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
var MySql_connect = builder.Configuration["MySql:Connect"]!;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        MySql_connect,
        ServerVersion.Parse("10.11.18-mariadb")
    ));

// Servicios
builder.Services.AddScoped<ILogProcesoService, LogProcesoService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
var JWT_key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(JWT_key)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline
//mwc if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// Captura cualquier ruta que no haya sido encontrada
app.MapFallback(async context =>
{
    context.Response.StatusCode = 404;
    context.Response.ContentType = "application/json";

    await context.Response.WriteAsJsonAsync(new
    {
        success = false,
        error = "Endpoint not found.",
        path = context.Request.Path,
        mensaje = "La ruta solicitada no existe en la API."
    });
});

app.Run();
