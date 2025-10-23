namespace WorkoutManager.DTOs;

public class WorkoutProgramDto
{
    public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ExerciseGroupDto> ExerciseGroups { get; set; } = new();
}

public class ExerciseGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public int WorkoutProgramId { get; set; }
    public List<ExerciseDto> Exercises { get; set; } = new();
}

