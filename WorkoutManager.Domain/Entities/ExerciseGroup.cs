using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

/**
 * Az osztály egy gyakorlatcsoportot reprezentál.
 */
public class ExerciseGroup : BaseEntity
{
    /**
     * A gyakorlatcsoport neve.
     */
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty; 
    
    // Navigation properties
    public ICollection<WorkoutProgram> WorkoutProgram { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
