using WorkoutManager.Application.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Edzésprogramot leíró adatátviteli objektum.
/// Tartalmazza az edzésprogram azonosítóját, kódnevét, címét, leírását, gyakorlatcsoportjait és időtartamait.
/// </summary>
public class WorkoutProgramDto
{
    /// <summary>
    /// Az edzésprogram egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Az edzésprogram kódneve.
    /// </summary>
    public string CodeName { get; set; } = string.Empty;
    /// <summary>
    /// Az edzésprogram címe.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Az edzésprogram részletes leírása.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Az edzésprogramhoz tartozó gyakorlatcsoportok listája.
    /// </summary>
    public List<ExerciseGroupDto> ExerciseGroups { get; set; } = new();
    /// <summary>
    /// Bemelegítés időtartama percben (opcionális).
    /// </summary>
    public int? WarmupDurationMinutes { get; set; }
    /// <summary>
    /// Fő edzés időtartama percben.
    /// </summary>
    public int MainWorkoutDurationMinutes { get; set; }
    /// <summary>
    /// Teljes edzésprogram időtartama percben (bemelegítés + fő edzés).
    /// </summary>
    public int TotalDurationMinutes => (WarmupDurationMinutes ?? 0) + MainWorkoutDurationMinutes;
}
