using System.Text;
using WorkoutManager.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WorkoutManager.Application.Extensions;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Entities;
using WorkoutManager.Extensions;
using WorkoutManager.Infrastructure.Extensions;
using WorkoutManager.Infrastructure.Services;
using WorkoutManager.Models;
using WorkoutManager.Seed;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Service hozzáadása az OpenAPI támogatáshoz
builder.Services.AddOpenApi();

// Swagger konfiguráció
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Workout Manager API",
        Description = "An ASP.NET Core Web API for managing workout programs, exercises, equipment, and contraindications",
        Contact = new OpenApiContact
        {
            Name = "Workout Manager Team",
            Email = "support@workoutmanager.com"
        }
    });

    // JWT Authentication konfiguráció Swaggerhez
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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

    // XML kommentek beolvasása a dokumentációhoz
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Konfigurálja a JSON szerializációs beállításokat
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// DbContext és egyéb infrastruktúra szolgáltatások hozzáadása
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IWorkoutProgramService, WorkoutProgramService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IExerciseGroupService, ExerciseGroupService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();
builder.Services.AddScoped<IContraindicationService, ContraindicationService>();
builder.Services.AddScoped<IContraindicationQueryService, ContraindicationQueryService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();

builder.Services.AddExceptionHandling();
builder.Services.AddResponseWrapper();

builder.Services.AddIdentityCore<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<WorkoutDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Controller hozzáadása
builder.Services.AddControllers();

builder.Services.AddAppServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Manager API v1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "Workout Manager API Documentation";
    });
}

// 19. feladathoz: Seed default admin user
using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedDefaultAdminAsync(scope.ServiceProvider);
}

app.UseExceptionHandling();
app.UseResponseWrapper();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorizedRequestLogging();

app.MapControllers();

app.MapGet("/hello", () => "Hello World!")
    .WithName("HelloWorld");

app.Run();
