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
    
    /**
     * Megadja a gyakorlatcsoport sorrendjét az edzésprogramon belül.
     */
    public int Order { get; set; }
    
    // Idegen kulcs a WorkoutProgram-hoz
    public int WorkoutProgramId { get; set; }
    
    // Navigation properties
    public WorkoutProgram WorkoutProgram { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}

