namespace WorkoutManager.DTOs;

public class EquipmentCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EquipmentDto> Equipment { get; set; } = new();
}

