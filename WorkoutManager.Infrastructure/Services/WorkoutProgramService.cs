using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.DTOs;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

public class WorkoutProgramService(IWorkoutProgramRepository workoutProgramRepository, IUnitOfWork unitOfWork,
    IExerciseGroupService exerciseGroupService)
    : CrudService<WorkoutProgram>(workoutProgramRepository, unitOfWork), IWorkoutProgramService
{ 
    public Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id)
    {
        var entity = workoutProgramRepository.GetFullWorkoutProgramAsync(id);
        return entity;
    }

    public async Task AssignExerciseGroupsAsync(int workoutProgramId, List<int> exerciseGroupIds)
    {
        var workoutProgram = await workoutProgramRepository.FirstOrDefaultAsync(wp => wp.Id == workoutProgramId);
        if (workoutProgram == null)
            throw new NotFoundException("Workout program not found");
        
        var exerciseGroups = await exerciseGroupService.GetAllByIdsAsync(exerciseGroupIds);
        
        workoutProgram.ExerciseGroups = exerciseGroups.ToList();

        workoutProgramRepository.Update(workoutProgram);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<WorkoutProgramWithContraindicationsDto>>
        GetAllTitleAndContraindicationByExerciseAsync(int exerciseId)
    {
        var entities = await workoutProgramRepository.GetAllTitleAndContraindicationByExerciseAsync(exerciseId);
        return entities
            .Select(MapToDtoWithContraindications)
            .ToList();
    }
    
    private static WorkoutProgramWithContraindicationsDto MapToDtoWithContraindications(WorkoutProgram wp)
    {
        // Az edzésprogram összes gyakorlata
        var allExercises = wp.ExerciseGroups
            .SelectMany(eg => eg.Exercises);

        // Ezekből gyűjtsd ki az összes kontraindikációt (gyakorlat + eszköz alapján)
        var contraindications = allExercises
            .SelectMany(e =>
                (e.Contraindications ?? new List<Contraindication>())
                .Concat(
                    e.Equipment?.Contraindications ?? new List<Contraindication>()
                )
            )
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .ToList();

        return new WorkoutProgramWithContraindicationsDto
        {
            Title = wp.Title,
            Contraindications = contraindications.Select(c => new ContraindicationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList()
        };
    }
}