using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutManager.Models;

/**
 * Az osztály egy gyakorlatot reprezentál.
 */
public class Exercise : BaseEntity
{
    /**
     * A gyakorlat neve.
     */
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 200 characters")]
    public string Name { get; set; } = string.Empty;
    
    /**
     * A gyakorlat ismétlés száma.
     */
    [Required(ErrorMessage = "Quantity is required")]
    [Range(0.01, 9999.99, ErrorMessage = "Quantity must be between 0.01 and 9999.99")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }
    
    /**
     * A gyakorlat mértékegysége.
     */
    [Required(ErrorMessage = "Unit is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Unit must be between 1 and 20 characters")]
    public string Unit { get; set; } = string.Empty;
    
    // Idegen kulcsok
    [Range(1, int.MaxValue, ErrorMessage = "EquipmentId must be greater than 0")]
    public int? EquipmentId { get; set; }
    
    // Navigation properties
    public ICollection<ExerciseGroup> ExerciseGroups { get; set; } = new List<ExerciseGroup>();
    public Equipment? Equipment { get; set; }
    public ICollection<Contraindication> Contraindications { get; set; } = new List<Contraindication>();
}
