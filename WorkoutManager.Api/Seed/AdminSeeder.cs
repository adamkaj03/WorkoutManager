using Microsoft.AspNetCore.Identity;
using WorkoutManager.Domain.Entities;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;

namespace WorkoutManager.Seed;

/// <summary>
/// Alapértelmezett adminisztrátor felhasználó létrehozására szolgáló seeder osztály.
/// Biztosítja, hogy az alkalmazás indításakor mindig legyen elérhető admin fiók.
/// </summary>
public static class AdminSeeder
{
    /// <summary>
    /// Alapértelmezett admin felhasználó és szerepkör létrehozása, ha még nem létezik.
    /// Ellenőrzi, hogy van-e Admin szerepkörű felhasználó, és ha nincs, létrehoz egyet.
    /// </summary>
    /// <param name="serviceProvider">A service provider a UserManager és RoleManager eléréséhez</param>
    public static async Task SeedDefaultAdminAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        const string adminRole = RoleNames.Admin;
        const string adminEmail = "admin@default.com";
        const string adminPassword = "Admin123!";

        // Admin szerepkör létrehozása, ha még nem létezik
        if (!await roleManager.RoleExistsAsync(adminRole))
            await roleManager.CreateAsync(new Role(adminRole));

        // Ellenőrzi, hogy van-e Admin szerepkörű felhasználó
        var admins = await userManager.GetUsersInRoleAsync(adminRole);
        if (admins.Count == 0)
        {
            // Alapértelmezett admin felhasználó létrehozása
            var adminUser = new User { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}