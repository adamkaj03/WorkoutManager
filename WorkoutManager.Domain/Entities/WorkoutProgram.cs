using System.ComponentModel.DataAnnotations;
using WorkoutManager.Domain.Attributes;

namespace WorkoutManager.Models;

/**
 * Az osztály egy edzésprogramot reprezentál.
 */
public class WorkoutProgram : BaseEntity
{
    /**
     * Az edzésprogram kódneve.
     */
    [Required(ErrorMessage = "CodeName is required")]
    [CodeNameValidation]
    public string CodeName { get; set; } = string.Empty;
    
    /**
     * Az edzésprogram címe.
     */
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public string Title { get; set; } = string.Empty;
    
    /**
     * Az edzésprogram leírása.
     */
    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters")]
    public string Description { get; set; } = string.Empty;
    

    /**
     * Bemelegítés ideje percben (opcionális).
     */
    [Range(1, 300, ErrorMessage = "WarmupDurationMinutes must be between 1 and 300")]
    public int? WarmupDurationMinutes { get; set; }

    /**
     * Fő edzés ideje percben.
     */
    [Required(ErrorMessage = "MainWorkoutDurationMinutes is required")]
    [Range(1, 600, ErrorMessage = "MainWorkoutDurationMinutes must be between 1 and 600")]
    public int MainWorkoutDurationMinutes { get; set; }
    
    // Navigation property
    public ICollection<ExerciseGroup> ExerciseGroups { get; set; } = new List<ExerciseGroup>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
