using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WorkoutManager.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddAppMapping();
        return services;
    }

    private static IServiceCollection AddAppMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        return services;
    }
}