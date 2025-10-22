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
    
    /**
     * Megadja a gyakorlat sorrendjét a gyakorlatcsoporton belül.
     */
    public int Order { get; set; }
    
    // Idegen kulcsok
    public int ExerciseGroupId { get; set; }
    public int? EquipmentId { get; set; }
    
    // Navigation properties
    public ExerciseGroup ExerciseGroup { get; set; } = null!;
    public Equipment? Equipment { get; set; }
    public ICollection<Contraindication> Contraindications { get; set; } = new List<Contraindication>();
}

