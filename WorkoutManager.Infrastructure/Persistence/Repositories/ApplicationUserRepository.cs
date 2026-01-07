using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Domain.Interfaces.Repositories;
using WorkoutManager.Models;

namespace WorkoutManager.Infrastructure.Persistence.Repositories;

public class ApplicationUserRepository(WorkoutDbContext db) : IApplicationUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        return await db.Users
            .Include(u => u.WorkoutPrograms)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public void Update(User user)
    {
        db.Users.Update(user);
    }
}