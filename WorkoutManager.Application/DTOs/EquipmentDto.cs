using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

/// <summary>
/// Edzőeszközt leíró adatátviteli objektum.
/// Tartalmazza az eszköz azonosítóját, nevét, leírását, kategóriáját és kontraindikációit.
/// </summary>
public class EquipmentDto
{
    /// <summary>
    /// Az eszköz egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Az eszköz neve.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Az eszköz részletes leírása.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Az eszköz kategóriájának azonosítója.
    /// </summary>
    public int EquipmentCategoryId { get; set; }
    /// <summary>
    /// Az eszköz kategóriájának neve.
    /// </summary>
    public string EquipmentCategoryName { get; set; } = string.Empty;
    /// <summary>
    /// Az eszközhöz tartozó kontraindikációk listája.
    /// </summary>
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}
