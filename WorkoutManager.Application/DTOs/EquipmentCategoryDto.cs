using WorkoutManager.Application.DTOs;

namespace WorkoutManager.DTOs;

/// <summary>
/// Edzőeszköz kategóriát leíró adatátviteli objektum.
/// Tartalmazza a kategória azonosítóját, nevét, leírását és az adott kategóriába tartozó eszközöket.
/// </summary>
public class EquipmentCategoryDto
{
    /// <summary>
    /// Az eszközkategória egyedi azonosítója.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Az eszközkategória neve.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Az eszközkategória részletes leírása.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Az adott kategóriába tartozó eszközök listája.
    /// </summary>
    public List<EquipmentDto> Equipment { get; set; } = new();
}
