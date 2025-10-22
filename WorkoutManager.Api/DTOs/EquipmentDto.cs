namespace WorkoutManager.DTOs;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EquipmentCategoryId { get; set; }
    public string EquipmentCategoryName { get; set; } = string.Empty;
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}

