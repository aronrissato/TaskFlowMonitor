using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using TaskFlow.API.Repositories;
using TaskFlow.API.Services;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// =======================================================
// 1 Config EntityFramework
// =======================================================

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =======================================================
// 2 Swagger
// =======================================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================================================
// 3 FluentValidation + AutoMapper
// =======================================================
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// =======================================================
// 4 DI
// =======================================================
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// =======================================================
// 5 Additional config
// =======================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// =======================================================
// 6 Middlewares
// =======================================================
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseRouting();

// Middleware Prometheus
app.UseHttpMetrics();
app.MapControllers();

// Endpoint Prometheus
app.MapMetrics(); // /metrics

app.Run();