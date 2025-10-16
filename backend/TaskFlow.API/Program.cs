using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using TaskFlow.API.Mappings;
using TaskFlow.API.Repositories;
using TaskFlow.API.Services;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Domain.Validators;
using TaskFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// =======================================================
// 1 Config EntityFramework
// =======================================================

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =======================================================
// 2 FluentValidation + AutoMapper
// =======================================================
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<TaskItemValidator>();
builder.Services.AddAutoMapper(typeof(TaskProfile).Assembly);

// =======================================================
// 3 DI
// =======================================================
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// =======================================================
// 4 Additional config
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
// 5 Middlewares
// =======================================================
app.UseCors("AllowAll");

app.UseRouting();

// Middleware Prometheus
app.UseHttpMetrics();
app.MapControllers();

// Endpoint Prometheus
app.MapMetrics(); // /metrics

app.Run();