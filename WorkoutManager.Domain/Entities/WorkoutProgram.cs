namespace WorkoutManager.Models;

/**
 * Az osztály egy edzésprogramot reprezentál.
 */
public class WorkoutProgram : BaseEntity
{
    /**
     * Az edzésprogram kódneve.
     */
    public string CodeName { get; set; } = string.Empty;
    
    /**
     * Az edzésprogram címe.
     */
    public string Title { get; set; } = string.Empty;
    
    /**
     * Az edzésprogram leírása.
     */
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<ExerciseGroup> ExerciseGroups { get; set; } = new List<ExerciseGroup>();
}

