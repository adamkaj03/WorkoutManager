using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;
using WorkoutManager.Shared.Exceptions;

namespace WorkoutManager.Infrastructure.Services;

/// <summary>
/// Gyakorlatok kezelésére szolgáló szolgáltatás.
/// Lehetővé teszi a gyakorlatok CRUD műveleteit, eszköz és kontraindikáció hozzárendelését,
/// valamint lekérdezést kontraindikációk alapján és gyorsítótárazást.
/// </summary>
public class ExerciseService : CrudService<Exercise>, IExerciseService
{
    private readonly IExerciseRepository exerciseRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEquipmentService equipmentService;
    private readonly IContraindicationService contraindicationService;
    private readonly ICacheService cacheService;

    /// <summary>
    /// Konstruktor, amely a szükséges repository-kat és szolgáltatásokat injektálja.
    /// </summary>
    public ExerciseService(
        IExerciseRepository exerciseRepository,
        IUnitOfWork unitOfWork,
        IEquipmentService equipmentService,
        IContraindicationService contraindicationService,
        ICacheService cacheService)
        : base(exerciseRepository, unitOfWork)
    {
        this.exerciseRepository = exerciseRepository;
        this.unitOfWork = unitOfWork;
        this.equipmentService = equipmentService;
        this.contraindicationService = contraindicationService;
        this.cacheService = cacheService;
    }

    /// <summary>
    /// Gyakorlatok lekérdezése egy adott kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationId">A kontraindikáció azonosítója.</param>
    /// <returns>A megadott kontraindikációval rendelkező gyakorlatok listája.</returns>
    public async Task<IEnumerable<Exercise>> GetExerciseByContraindicationAsync(int contraindicationId)
        => await exerciseRepository.GetByContraindicationAsync(contraindicationId);

    /// <summary>
    /// Gyakorlatok lekérdezése több kontraindikáció alapján.
    /// </summary>
    /// <param name="contraindicationIds">A kontraindikációk azonosítóinak listája.</param>
    /// <returns>A megadott kontraindikációkkal rendelkező gyakorlatok listája.</returns>
    public async Task<IEnumerable<Exercise>> GetByContraindicationsAsync(List<int> contraindicationIds)
        => await exerciseRepository.GetByContraindicationsAsync(contraindicationIds);

    /// <summary>
    /// Minden gyakorlat lekérdezése, gyorsítótárazva (ha nincs törölt is kérve).
    /// </summary>
    /// <param name="includeDeleted">Tartalmazza-e a törölt gyakorlatokat.</param>
    /// <returns>A gyakorlatok listája.</returns>
    public new async Task<IEnumerable<Exercise>> GetAllAsync(bool includeDeleted = false)
    {
        if (includeDeleted)
            return await base.GetAllAsync(includeDeleted);

        return await cacheService.GetOrCreateAsync(
            CacheKeyNames.AllExercises,
            async () => await base.GetAllAsync(includeDeleted),
            absoluteExpireTime: TimeSpan.FromHours(1)
        ) ?? new List<Exercise>();
    }

    /// <summary>
    /// Új gyakorlat létrehozása, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="entity">A létrehozandó gyakorlat.</param>
    /// <returns>A létrehozott gyakorlat.</returns>
    public new async Task<Exercise> CreateAsync(Exercise entity)
    {
        var result = await base.CreateAsync(entity);
        cacheService.Remove(CacheKeyNames.AllExercises);
        return result;
    }

    /// <summary>
    /// Gyakorlat frissítése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A frissítendő gyakorlat azonosítója.</param>
    /// <param name="entity">A frissített gyakorlat adatai.</param>
    /// <returns>A frissített gyakorlat.</returns>
    public new async Task<Exercise> UpdateAsync(int id, Exercise entity)
    {
        var result = await base.UpdateAsync(id, entity);
        cacheService.Remove(CacheKeyNames.AllExercises);
        return result;
    }

    /// <summary>
    /// Gyakorlat törlése, majd a gyorsítótár frissítése.
    /// </summary>
    /// <param name="id">A törlendő gyakorlat azonosítója.</param>
    public new async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
        cacheService.Remove(CacheKeyNames.AllExercises);
    }

    /// <summary>
    /// Eszköz hozzárendelése egy gyakorlathoz.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <param name="equipmentId">A hozzárendelendő eszköz azonosítója.</param>
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
        cacheService.Remove(CacheKeyNames.AllExercises);
    }
    
    /// <summary>
    /// Kontraindikációk hozzárendelése egy gyakorlathoz.
    /// </summary>
    /// <param name="exerciseId">A gyakorlat azonosítója.</param>
    /// <param name="contraindicationIds">A hozzárendelendő kontraindikációk azonosítóinak listája.</param>
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
        cacheService.Remove(CacheKeyNames.AllExercises);
    }
}