namespace WorkoutManager.Models;

/**
 * Az osztály egy eszköz kategóriát reprezentál.
 */
public class EquipmentCategory : BaseEntity
{
    /**
     * Az eszköz kategória neve.
     */
    public string Name { get; set; } = string.Empty;
    
    /**
     * Az eszköz kategória leírása.
     */
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}

