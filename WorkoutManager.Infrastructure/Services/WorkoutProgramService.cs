using WorkoutManager.Application.DTOs;
using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.DTOs;
using WorkoutManager.Models;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Edzésprogramok kezelésére szolgáló szolgáltatás.
/// Lehetővé teszi az edzésprogramok CRUD műveleteit, teljes program lekérdezését,
/// gyakorlatcsoportok hozzárendelését, valamint cím és kontraindikáció lekérdezését gyakorlat alapján.
/// </summary>
public class WorkoutProgramService : CrudService<WorkoutProgram>, IWorkoutProgramService
{
    private readonly IWorkoutProgramRepository workoutProgramRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IExerciseGroupService exerciseGroupService;

    /// <summary>
    /// Konstruktor, amely a szükséges repository-kat és szolgáltatásokat injektálja.
    /// </summary>
    public WorkoutProgramService(
        IWorkoutProgramRepository workoutProgramRepository,
        IUnitOfWork unitOfWork,
        IExerciseGroupService exerciseGroupService)
        : base(workoutProgramRepository, unitOfWork)
    {
        this.workoutProgramRepository = workoutProgramRepository;
        this.unitOfWork = unitOfWork;
        this.exerciseGroupService = exerciseGroupService;
    }

    /// <summary>
    /// Egy teljes edzésprogram lekérdezése az összes kapcsolódó adattal.
    /// </summary>
    /// <param name="id">Az edzésprogram azonosítója.</param>
    /// <returns>Az edzésprogram összes kapcsolódó adattal, vagy null, ha nem található.</returns>
    public Task<WorkoutProgram?> GetFullWorkoutProgramAsync(int id)
    {
        var entity = workoutProgramRepository.GetFullWorkoutProgramAsync(id);
        return entity;
    }

    /// <summary>
    /// Gyakorlatcsoportok hozzárendelése egy edzésprogramhoz.
    /// </summary>
    /// <param name="workoutProgramId">Az edzésprogram azonosítója.</param>
    /// <param name="exerciseGroupIds">A hozzárendelendő gyakorlatcsoportok azonosítói.</param>
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

    /// <summary>
    /// Edzésprogramok címének és kontraindikációinak lekérdezése egy adott gyakorlat alapján.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <returns>Az edzésprogramok címe és kontraindikációi.</returns>
    public async Task<IEnumerable<WorkoutProgramWithContraindicationsDto>>
        GetAllTitleAndContraindicationByExerciseAsync(int exerciseId)
    {
        var entities = await workoutProgramRepository.GetAllTitleAndContraindicationByExerciseAsync(exerciseId);
        return entities
            .Select(MapToDtoWithContraindications)
            .ToList();
    }
    
    /// <summary>
    /// Edzésprogram entitás DTO-vá alakítása, amely tartalmazza a címét és a kapcsolódó kontraindikációkat.
    /// </summary>
    /// <param name="wp">Az edzésprogram entitás.</param>
    /// <returns>DTO a címmel és kontraindikációkkal.</returns>
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
    
    /// <summary>
    /// Edzésprogramok lekérdezése azonosítók alapján.
    /// </summary>
    /// <param name="workoutProgramIds">Az edzésprogramok azonosítóinak listája.</param>
    /// <returns>Az edzésprogramok listája.</returns>
    public async Task<IEnumerable<WorkoutProgram>> GetAllByIdsAsync(List<int> workoutProgramIds)
    {
        return await workoutProgramRepository.ListAsync(wp => workoutProgramIds.Contains(wp.Id));
    }
}