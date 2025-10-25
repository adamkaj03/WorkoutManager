using WorkoutManager.DTOs;

namespace WorkoutManager.Application.DTOs;

public class WorkoutProgramWithContraindicationsDto
{
    public string Title { get; set; } = string.Empty;
    public List<ContraindicationDto> Contraindications { get; set; }
}