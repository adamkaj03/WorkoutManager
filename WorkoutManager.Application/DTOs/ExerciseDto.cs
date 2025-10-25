namespace WorkoutManager.DTOs;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public EquipmentDto? Equipment { get; set; }
    public List<ContraindicationDto> Contraindications { get; set; } = new();
}

