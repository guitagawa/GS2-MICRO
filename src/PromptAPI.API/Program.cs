using FluentValidation;
using MediatR;
using PromptAPI.API.Middleware;
using PromptAPI.Application.Behaviors;
using PromptAPI.Domain.Interfaces;
using PromptAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configuração do MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(PromptAPI.Application.DTOs.PromptDto).Assembly);
});

// Configuração do FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(PromptAPI.Application.DTOs.PromptDto).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Injeção de Dependência - Repositórios
builder.Services.AddScoped<IPromptRepository, PromptRepository>();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PromptAPI",
        Version = "v1",
        Description = "API para gerenciamento de prompts com Clean Architecture e CQRS"
    });
});

// Configuração de CORS (opcional, mas recomendado)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware de tratamento de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PromptAPI v1");
        options.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
