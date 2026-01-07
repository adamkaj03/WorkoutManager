using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

/**
 * Az osztály egy eszközt reprezentál.
 */
public class Equipment : BaseEntity
{
    /**
     * AZ eszköz neve.
     */
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    /**
     * Az eszköz leírása.
     */
    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    public string Description { get; set; } = string.Empty;
    
    // Idegen kulcs az EquipmentCategory-hoz
    [Required(ErrorMessage = "EquipmentCategoryId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "EquipmentCategoryId must be greater than 0")]
    public int EquipmentCategoryId { get; set; }
    
    // Navigation properties
    public EquipmentCategory EquipmentCategory { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<Contraindication> Contraindications { get; set; } = new List<Contraindication>();
}
