using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

public class ExerciseGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ExerciseDto> Exercises { get; set; } = new();
}