namespace WorkoutManager.DTOs;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int Order { get; set; }
    public int ExerciseGroupId { get; set; }
    public string ExerciseGroupName { get; set; } = string.Empty;
    public int? EquipmentId { get; set; }
    public string? EquipmentName { get; set; }
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}

