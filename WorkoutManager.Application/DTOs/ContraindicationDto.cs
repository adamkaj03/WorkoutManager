namespace WorkoutManager.DTOs;

/// <summary>
/// Kontraindikációt leíró adatátviteli objektum.
/// Tartalmazza a kontraindikáció azonosítóját, nevét és leírását.
/// </summary>
public class ContraindicationDto
{
    /// <summary>
    /// A kontraindikáció egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// A kontraindikáció neve.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// A kontraindikáció részletes leírása.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
