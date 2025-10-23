using WorkoutManager.Application.DTOs;

namespace WorkoutManager.DTOs;

public class WorkoutProgramDto
{
    //public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ExerciseGroupDto> ExerciseGroups { get; set; } = new();
}

