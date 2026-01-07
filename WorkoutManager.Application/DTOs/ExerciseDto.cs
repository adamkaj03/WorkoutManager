using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Gyakorlatot leíró adatátviteli objektum.
/// Tartalmazza a gyakorlat azonosítóját, nevét, mennyiségét, mértékegységét, eszközét és kontraindikációit.
/// </summary>
public class ExerciseDto
{
    /// <summary>
    /// A gyakorlat egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// A gyakorlat neve.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// A gyakorlat mennyisége (pl. ismétlésszám, időtartam).
    /// </summary>
    public decimal Quantity { get; set; }
    /// <summary>
    /// A gyakorlat mértékegysége (pl. db, perc).
    /// </summary>
    public string Unit { get; set; } = string.Empty;
    /// <summary>
    /// A gyakorlat során használt eszköz.
    /// </summary>
    public EquipmentDto? Equipment { get; set; }
    /// <summary>
    /// A gyakorlat kontraindikációinak listája.
    /// </summary>
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}
