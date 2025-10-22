namespace WorkoutManager.Models;

/**
 * Az osztály egy eszközt reprezentál.
 */
public class Equipment : BaseEntity
{
    /**
     * AZ eszköz neve.
     */
    public string Name { get; set; } = string.Empty;
    
    /**
     * Az eszköz leírása.
     */
    public string Description { get; set; } = string.Empty;
    
    // Idegen kulcs az EquipmentCategory-hoz
    public int EquipmentCategoryId { get; set; }
    
    // Navigation properties
    public EquipmentCategory EquipmentCategory { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<Contraindication> Contraindications { get; set; } = new List<Contraindication>();
}

