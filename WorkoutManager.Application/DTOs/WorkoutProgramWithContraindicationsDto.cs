using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Edzésprogramot és hozzá tartozó kontraindikációkat leíró adatátviteli objektum.
/// Tartalmazza az edzésprogram címét és a kapcsolódó kontraindikációk listáját.
/// </summary>
public class WorkoutProgramWithContraindicationsDto
{
    /// <summary>
    /// Az edzésprogram címe.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Az edzésprogramhoz tartozó kontraindikációk listája.
    /// </summary>
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}