using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tpo_DotNet_bb.Api.Api.Entities;
using Tpo_DotNet_bb.Api.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// CORS
// =====================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFront", policy =>
    {
        policy
            .WithOrigins(
                "https://tpo-nodejs-bf.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// =====================================================
// BASE DE DATOS
// =====================================================
var MySql_connect =
    builder.Configuration["MySql:Connect"]!;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        MySql_connect,
        ServerVersion.Parse("10.11.18-mariadb")
    ));

// =====================================================
// SERVICIOS
// =====================================================
builder.Services.AddScoped
<
    ILogProcesoService,
    LogProcesoService
>();

// =====================================================
// CONTROLLERS
// =====================================================
builder.Services.AddControllers();

// =====================================================
// SWAGGER
// =====================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================================
// JWT
// =====================================================
var JWT_key = Encoding.UTF8.GetBytes(
    builder.Configuration["Jwt:Key"]!
);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(JWT_key),

                ClockSkew = TimeSpan.Zero
            };

        // ==========================================
        // LEER JWT DESDE COOKIE
        // ==========================================
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token =
                    context.Request.Cookies["tpo_dotnet_bb"];

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// =====================================================
// APP
// =====================================================
var app = builder.Build();

// =====================================================
// SWAGGER
// =====================================================
app.UseSwagger();
app.UseSwaggerUI();

// =====================================================
// PIPELINE
// =====================================================
app.UseHttpsRedirection();

app.UseCors("AllowFront");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// =====================================================
// ERROR 404
// =====================================================
app.MapFallback(async context =>
{
    context.Response.StatusCode = 404;
    context.Response.ContentType = "application/json";

    await context.Response.WriteAsJsonAsync(new
    {
        success = false,
        error = "Endpoint Not Found",
        path = context.Request.Path,
        mensaje = "La ruta solicitada no existe en la API"
    });
});

app.Run();
