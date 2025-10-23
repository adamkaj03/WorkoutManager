using WorkoutManager.Application.Interfaces;
using WorkoutManager.Domain.Interfaces;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Services;

public class ContraindicationService(IContraindicationRepository contraindicationRepository, IUnitOfWork unitOfWork)
    : CrudService<Contraindication>(contraindicationRepository, unitOfWork), IContraindicationService
{
    
}