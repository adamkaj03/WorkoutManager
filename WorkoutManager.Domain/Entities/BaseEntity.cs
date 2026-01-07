using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

public abstract class BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}