using WorkoutManager.Middlewares;

namespace WorkoutManager.Extensions;

/// <summary>
/// Middleware-ek regisztrálására és használatára szolgáló extension metódusok.
/// Egyszerűsíti a middleware-ek hozzáadását az alkalmazáshoz.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Kivételkezelő middleware regisztrálása a dependency injection konténerben.
    /// </summary>
    /// <param name="services">A service collection</param>
    /// <returns>A módosított service collection</returns>
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
        return services;
    }
    
    /// <summary>
    /// Válasz becsomagoló middleware regisztrálása a dependency injection konténerben.
    /// </summary>
    /// <param name="services">A service collection</param>
    /// <returns>A módosított service collection</returns>
    public static IServiceCollection AddResponseWrapper(this IServiceCollection services)
    {
        services.AddTransient<ResponseWrapperMiddleware>();
        return services;
    }
    
    /// <summary>
    /// Kivételkezelő middleware hozzáadása az alkalmazás pipeline-jához.
    /// Ez a middleware elkapja és kezeli az alkalmazásban fellépő kivételeket.
    /// </summary>
    /// <param name="app">Az application builder</param>
    /// <returns>A módosított application builder</returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    
    /// <summary>
    /// Válasz becsomagoló middleware hozzáadása az alkalmazás pipeline-jához.
    /// Ez a middleware egységes formátumba csomagolja a válaszokat.
    /// </summary>
    /// <param name="app">Az application builder</param>
    /// <returns>A módosított application builder</returns>
    public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ResponseWrapperMiddleware>();
    }
    
    /// <summary>
    /// Autorizált kérések naplózására szolgáló middleware hozzáadása az alkalmazás pipeline-jához.
    /// Ez a middleware logolja az autorizációt igénylő végpontokra érkező kéréseket.
    /// </summary>
    /// <param name="app">Az application builder</param>
    /// <returns>A módosított application builder</returns>
    public static IApplicationBuilder UseAuthorizedRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthorizedRequestLoggingMiddleware>();
    }
}