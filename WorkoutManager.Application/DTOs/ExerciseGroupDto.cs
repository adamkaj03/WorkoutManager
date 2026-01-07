using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Gyakorlatcsoportot leíró adatátviteli objektum.
/// Tartalmazza a csoport azonosítóját, nevét és a csoporthoz tartozó gyakorlatokat.
/// </summary>
public class ExerciseGroupDto
{
    /// <summary>
    /// A gyakorlatcsoport egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// A gyakorlatcsoport neve.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// A csoporthoz tartozó gyakorlatok listája.
    /// </summary>
    public List<ExerciseDto> Exercises { get; set; } = new();
}