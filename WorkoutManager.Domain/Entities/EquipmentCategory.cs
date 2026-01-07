using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Models;

/**
 * Az osztály egy eszköz kategóriát reprezentál.
 */
public class EquipmentCategory : BaseEntity
{
    /**
     * Az eszköz kategória neve.
     */
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    /**
     * Az eszköz kategória leírása.
     */
    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
