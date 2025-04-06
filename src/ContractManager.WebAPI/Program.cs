using ContractManager.Application;
using ContractManager.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Configurações do JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// 🔧 Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 🔧 Swagger + JWT + Annotations + Examples
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ContractManager API",
        Version = "v1",
        Description = "API para gestão de contratos e documentos com autenticação JWT"
    });

    // 🔐 JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Ativa uso de [SwaggerSchema], [SwaggerOperation], etc.
    options.EnableAnnotations();

    // Suporte a exemplos automáticos (caso esteja usando IExamplesProvider<>)
    options.ExampleFilters();
});

// Adiciona exemplos de DTOs
builder.Services.AddSwaggerExamplesFromAssemblyOf<ContractManager.Application.DTOs.UploadDocumentDto>();

// 🔐 Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// 📦 Injeção de dependências
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 🚀 Build
var app = builder.Build();

// 🌍 Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContractManager API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // ⚠️ Antes do Authorization
app.UseAuthorization();
app.MapControllers();
app.Run();
