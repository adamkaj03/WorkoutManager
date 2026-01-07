using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Middlewares;

/// <summary>
/// Middleware a globális kivételkezeléshez.
/// Elkapja az alkalmazásban fellépő kivételeket és megfelelő HTTP válaszokat ad vissza.
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    /// <summary>
    /// A middleware végrehajtása minden HTTP kérés esetén.
    /// Elkapja a kivételeket és megfelelő státuszkóddal tér vissza.
    /// </summary>
    /// <param name="context">A HTTP kontextus</param>
    /// <param name="next">A következő middleware a pipeline-ban</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            // 404 Not Found válasz, ha az erőforrás nem található
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
    }
}