namespace WorkoutManager.Models;

/**
 * Az osztály egy gyakorlatot reprezentál.
 */
public class Exercise : BaseEntity
{
    /**
     * A gyakorlat neve.
     */
    public string Name { get; set; } = string.Empty;
    
    /**
     * A gyakorlat ismétlés száma.
     */
    public decimal Quantity { get; set; }
    
    /**
     * A gyakorlat mértékegysége.
     */
    public string Unit { get; set; } = string.Empty;
    
    // Idegen kulcsok
    public int? EquipmentId { get; set; }
    
    // Navigation properties
    public ICollection<ExerciseGroup> ExerciseGroups { get; set; } = new List<ExerciseGroup>();
    public Equipment? Equipment { get; set; }
    public ICollection<Contraindication> Contraindications { get; set; } = new List<Contraindication>();
}

