using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using System.Text.Json.Serialization;
using WorkoutManager.Application.Extensions;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Infrastructure.Extensions;
using WorkoutManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure JSON serialization to handle circular references
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Add DbContext
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IWorkoutProgramService, WorkoutProgramService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IExerciseGroupService, ExerciseGroupService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();
builder.Services.AddScoped<IContraindicationService, ContraindicationService>();

// Add controllers
builder.Services.AddControllers();

builder.Services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/hello", () => "Hello World!")
    .WithName("HelloWorld");

app.Run();
