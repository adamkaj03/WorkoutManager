using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WorkoutManager.Middlewares;

/// <summary>
/// Middleware az autorizációt igénylő végpontokra érkező kérések naplózására.
/// Csak azokat a kéréseket logolja, amelyek autorizált végpontokat hívnak meg.
/// </summary>
public class AuthorizedRequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthorizedRequestLoggingMiddleware> _logger;

    /// <summary>
    /// Az AuthorizedRequestLoggingMiddleware konstruktora.
    /// </summary>
    /// <param name="next">A következő middleware a pipeline-ban</param>
    /// <param name="logger">Logger a kérések naplózásához</param>
    public AuthorizedRequestLoggingMiddleware(RequestDelegate next, ILogger<AuthorizedRequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// A middleware végrehajtása minden HTTP kérés esetén.
    /// Ellenőrzi, hogy a végpont autorizációt igényel-e, és ha igen, naplózza a kérés részleteit.
    /// </summary>
    /// <param name="context">A HTTP kontextus</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var requiresAuth = endpoint?.Metadata.GetMetadata<IAuthorizeData>() != null;

        // Csak akkor naplóz, ha a végpont autorizációt igényel és a felhasználó be van jelentkezve
        if (requiresAuth && context.User.Identity?.IsAuthenticated == true)
        {
            // Request body újraolvasásának engedélyezése
            context.Request.EnableBuffering();
            
            var method = context.Request.Method;
            var endpointName = endpoint?.DisplayName ?? context.Request.Path;
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = context.User.Identity?.Name;

            // Request body olvasása
            string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0; // Stream pozíció visszaállítása

            var logMessage = $"[Authorized Request]\n" +
                             $"Method: {method}\n" +
                             $"Endpoint: {endpointName}\n" +
                             $"User ID: {userId}\n" +
                             $"User Name: {userName}\n" +
                             $"Body: {requestBody}";

            _logger.LogInformation(logMessage);
        }

        await _next(context);
    }
}
