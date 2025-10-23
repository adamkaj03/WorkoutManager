namespace WorkoutManager.DTOs;

public class ContraindicationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ContraindicationDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> RelatedExercises { get; set; } = new();
    public List<string> RelatedEquipment { get; set; } = new();
}
