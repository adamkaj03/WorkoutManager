namespace WorkoutManager.DTOs;

public class EquipmentCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EquipmentSimpleDto> Equipment { get; set; } = new();
}

public class EquipmentSimpleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

