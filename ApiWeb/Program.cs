using Infrastructure.Data;

using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

// Registrar DbContext usando MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

// 🔐 Configurar autenticación JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

// Registrar servicios de aplicación
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<ILessonService, LessonService>();

// Registrar repositorios de infraestructura
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();

// Servicios básicos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT con el prefijo 'Bearer '. Ej: Bearer abc123",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Activar middlewares
app.UseSwagger();
app.UseSwaggerUI();

// Middleware global para restringir por IP
app.Use(async (context, next) =>
{
    var allowedIp = "187.155.101.200";
    var remoteIp = context.Connection.RemoteIpAddress?.ToString();
    if (remoteIp != allowedIp)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Lo sentimos esta BELLA API NO ESTA DISPONIBLE EN TU IP :)");
        return;
    }
    await next();
});

// 🛡️ Activar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
