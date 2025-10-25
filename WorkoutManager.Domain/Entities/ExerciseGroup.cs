namespace WorkoutManager.Models;

/**
 * Az osztály egy gyakorlatcsoportot reprezentál.
 */
public class ExerciseGroup : BaseEntity
{
    /**
     * A gyakorlatcsoport neve.
     */
    public string Name { get; set; } = string.Empty; 
    
    // Navigation properties
    public ICollection<WorkoutProgram> WorkoutProgram { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}

