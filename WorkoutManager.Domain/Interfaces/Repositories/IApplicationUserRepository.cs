using WorkoutManager.Models;

namespace WorkoutManager.Domain.Interfaces.Repositories;

public interface IApplicationUserRepository
{
    Task<User?> GetByIdAsync(string id);
    void Update(User user);
}