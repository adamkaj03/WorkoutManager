namespace WorkoutManager.Middlewares;

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Middleware a HTTP válaszok egységes formátumba csomagolására.
/// Minden választ JSON objektumba csomagol, amely tartalmazza a státuszkódot, tartalmat és egyedi azonosítót.
/// </summary>
public class ResponseWrapperMiddleware : IMiddleware
{
    /// <summary>
    /// Megpróbálja JSON objektumként értelmezni a válasz törzsét.
    /// Ha sikeres, JSON objektumot ad vissza, különben az eredeti string-et.
    /// </summary>
    /// <param name="responseBody">A válasz törzse string formátumban</param>
    /// <returns>Deszerializált JSON objektum vagy az eredeti string</returns>
    private object TryParseJson(string responseBody)
    {
        try
        {
            return JsonSerializer.Deserialize<object>(responseBody);
        }
        catch
        {
            // Ha nem sikerült JSON-ként értelmezni, az eredeti string-et adjuk vissza
            return responseBody;
        }
    }

    /// <summary>
    /// A middleware végrehajtása minden HTTP kérés esetén.
    /// Elfogja a választ, becsomagolja egységes formátumba és visszaküldi.
    /// </summary>
    /// <param name="context">A HTTP kontextus</param>
    /// <param name="next">A következő middleware a pipeline-ban</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Egyedi azonosító generálása minden válaszhoz
        var identity = Guid.NewGuid();
        var originalBodyStream = context.Response.Body;

        // Memória stream használata a válasz elfogására
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        // Következő middleware végrehajtása
        await next(context);

        // Válasz olvasása a memória streamből
        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

        // Válasz becsomagolása egységes formátumba
        var wrappedResponse = new
        {
            statusCode = context.Response.StatusCode,
            content = TryParseJson(responseBody),
            identity = identity
        };

        // Becsomagolt válasz visszaírása az eredeti streambe
        context.Response.Body = originalBodyStream;
        context.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(wrappedResponse);
        await context.Response.WriteAsync(json);
    }
}
