using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

public class ExerciseService(IExerciseRepository exerciseRepository,
    IUnitOfWork unitOfWork, IEquipmentService equipmentService, IContraindicationService contraindicationService) 
    : CrudService<Exercise>(exerciseRepository, unitOfWork), IExerciseService
{
    public async Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId)
        => await exerciseRepository.GetByContraindicationAsync(contraindicationId);

    public async Task AssignEquipmentAsync(int exerciseId, int equipmentId)
    {
        var exercise = await exerciseRepository.FirstOrDefaultAsync(exercise => exercise.Id == exerciseId);
        if (exercise == null)
            throw new NotFoundException("Exercise not found");

        var equipment = await equipmentService.GetByIdAsync(equipmentId);
        if (equipment == null)
            throw new NotFoundException("Equipment not found");

        // 1:1 kapcsolat (egy exercise-hoz egy equipment):
        exercise.Equipment = equipment;

        exerciseRepository.Update(exercise);
        await unitOfWork.SaveChangesAsync();
    }
    
    public async Task AssignContraindicationsAsync(int exerciseId, List<int> contraindicationIds)
    {
        var exercise = await exerciseRepository.FirstOrDefaultAsync(e => e.Id == exerciseId);
        if (exercise == null)
            throw new NotFoundException("Exercise not found");

        // Lekérjük a kontraindikációkat a listából
        var contraindications = await contraindicationService.GetAllByIdsAsync(contraindicationIds);

        // Felülírjuk a kapcsolódó kontraindikációkat
        exercise.Contraindications = contraindications.ToList();

        exerciseRepository.Update(exercise);
        await unitOfWork.SaveChangesAsync();
    }
}