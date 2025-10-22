namespace WorkoutManager.Models;

/**
 * Az osztály a kontraindikációkat reprezentálja.
 */
public class Contraindication : BaseEntity
{
    /**
     * Kontraindikáció neve.
     */
    public string Name { get; set; } = string.Empty; // e.g., "Back pain", "Shoulder injury", "Knee problem"
    
    /**
     * Kontraindikáció leírása.
     */
    public string Description { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}

