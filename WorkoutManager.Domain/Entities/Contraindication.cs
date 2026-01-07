using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

/**
 * Az osztály a kontraindikációkat reprezentálja.
 */
public class Contraindication : BaseEntity
{
    /**
     * Kontraindikáció neve.
     */
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty; // e.g., "Back pain", "Shoulder injury", "Knee problem"
    
    /**
     * Kontraindikáció leírása.
     */
    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    public string Description { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}

