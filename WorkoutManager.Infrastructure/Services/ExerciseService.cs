using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork) 
    : CrudService<Exercise>(exerciseRepository, unitOfWork), IExerciseService
{
    public async Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId)
        => await exerciseRepository.GetByContraindicationAsync(contraindicationId);
}