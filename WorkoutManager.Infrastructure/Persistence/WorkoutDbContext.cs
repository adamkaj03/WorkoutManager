using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Domain.Entities;
using WorkoutManager.Models;
using WorkoutManager.Shared.Constants;

namespace WorkoutManager.Data;

public class WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Models.WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<Models.ExerciseGroup> ExerciseGroups { get; set; }
    public DbSet<Models.Exercise> Exercises { get; set; }
    public DbSet<Models.Equipment> Equipment { get; set; }
    public DbSet<Models.EquipmentCategory> EquipmentCategories { get; set; }
    public DbSet<Models.Contraindication> Contraindications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
            entity.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                // FONTOS: Ne legyen itt is CASCADE, legyen Restrict vagy NoAction!
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<WorkoutProgram>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CodeName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.MainWorkoutDurationMinutes).IsRequired();
            
            // Many-to-many
            entity.HasMany(e => e.ExerciseGroups)
                .WithMany(eq => eq.WorkoutProgram)
                .UsingEntity(j => j.ToTable("WorkoutProgramExerciseGroups"));
            
            entity.HasMany(e => e.Users)
                .WithMany(eq => eq.WorkoutPrograms)
                .UsingEntity(j => j.ToTable("WorkoutProgramApplicationUsers"));
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        modelBuilder.Entity<ExerciseGroup>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
        
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Unit).IsRequired().HasMaxLength(20);

            // Many-to-One
            entity.HasMany(e => e.ExerciseGroups)
                .WithMany(eq => eq.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseExerciseGroups"));

            // Many-to-Many
            entity.HasMany(e => e.Contraindications)
                .WithMany(c => c.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseContraindications"));
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
        
        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(e => e.EquipmentCategory)
                .WithMany(ec => ec.Equipment)
                .HasForeignKey(e => e.EquipmentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many
            entity.HasMany(e => e.Contraindications)
                .WithMany(c => c.Equipment)
                .UsingEntity(j => j.ToTable("EquipmentContraindications"));
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
        
        modelBuilder.Entity<EquipmentCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
        
        modelBuilder.Entity<Contraindication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(e => e.WorkoutPrograms)
                .WithMany(wp => wp.Users)
                .UsingEntity(j => j.ToTable("WorkoutProgramUsers"));
        });
        
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = "2847bd82-071c-44b2-8f6b-5540c7d70370", Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpper() },
            new Role { Id = "2bae9d45-61c1-44ba-9e26-9ad7b0752b53", Name = RoleNames.Writer, NormalizedName = RoleNames.Writer.ToUpper() },
            new Role { Id = "1ccd9ab3-6fb3-445e-ac76-2dc8b806232e", Name = RoleNames.Reader, NormalizedName = RoleNames.Reader.ToUpper() }
        );
        
        modelBuilder.Entity<Contraindication>().HasData(
            new Contraindication { Id = 1, Name = "High Blood Pressure", Description = "Avoid high-intensity exercises." },
            new Contraindication { Id = 2, Name = "Knee Injury", Description = "Avoid exercises that put strain on the knees." },
            new Contraindication { Id = 3, Name = "Back Pain", Description = "Avoid heavy lifting and twisting movements." },
            new Contraindication { Id = 4, Name = "Heart Condition", Description = "Avoid strenuous cardiovascular activities." },
            new Contraindication { Id = 5, Name = "Pregnancy", Description = "Avoid exercises that involve lying flat on the back after the first trimester." }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Treadmill", Description = "A machine for walking or running while staying in one place.", EquipmentCategoryId = 1 },
            new Equipment { Id = 2, Name = "Dumbbells", Description = "A type of free weight used in weight training.", EquipmentCategoryId = 2 },
            new Equipment { Id = 3, Name = "Yoga Mat", Description = "A mat used for yoga practice.", EquipmentCategoryId = 2 },
            new Equipment { Id = 4, Name = "Exercise Bike", Description = "A stationary bike used for cardiovascular workouts.", EquipmentCategoryId = 3 },
            new Equipment { Id = 5, Name = "Resistance Bands", Description = "Elastic bands used for strength training.", EquipmentCategoryId = 2 },
            new Equipment { Id = 6, Name = "None", Description = "Not required.", EquipmentCategoryId = 4 }
        );

        modelBuilder.Entity<EquipmentCategory>().HasData(
            new EquipmentCategory{ Id = 1, Name = "Cardio", Description = "Equipment used for cardiovascular workouts." },
            new EquipmentCategory{ Id = 2, Name = "Strength", Description = "Equipment used for strength and resistance training." },
            new EquipmentCategory{ Id = 3, Name = "Flexibility", Description = "Equipment used for improving flexibility and balance." },
            new EquipmentCategory{ Id = 4, Name = "None", Description = "No equipment required." }
        );
        
        modelBuilder.Entity<ExerciseGroup>().HasData(
            new ExerciseGroup { Id = 1, Name = "Warm-up Exercises" },
            new ExerciseGroup { Id = 2, Name = "Main Exercises" },
            new ExerciseGroup { Id = 3, Name = "Strength Training" },
            new ExerciseGroup { Id = 4, Name = "Cool-down Exercises" }
        );
        
        modelBuilder.Entity<Exercise>().HasData(
            new Exercise { Id = 1, Name = "Jumping Jacks", Quantity = 30, Unit = "reps", EquipmentId = 6},
            new Exercise { Id = 2, Name = "Push-ups", Quantity = 15, Unit = "reps", EquipmentId = 6 },
            new Exercise { Id = 3, Name = "Squats", Quantity = 20, Unit = "reps", EquipmentId = 2 },
            new Exercise { Id = 4, Name = "Plank", Quantity = 60, Unit = "seconds", EquipmentId = 6 },
            new Exercise { Id = 5, Name = "Lunges", Quantity = 20, Unit = "reps", EquipmentId = 2 },
            new Exercise { Id = 6, Name = "Burpees", Quantity = 10, Unit = "reps", EquipmentId = 4 },
            new Exercise { Id = 7, Name = "Mountain Climbers", Quantity = 30, Unit = "seconds", EquipmentId = 1 },
            new Exercise { Id = 8, Name = "High Knees", Quantity = 30, Unit = "seconds", EquipmentId = 1 }
        );

        modelBuilder.Entity<WorkoutProgram>().HasData(
            new WorkoutProgram { Id = 1, CodeName = "BEGINNER_FULL_BODY", Title = "Beginner Full Body Workout", Description = "A full body workout program for beginners.", MainWorkoutDurationMinutes = 30 , WarmupDurationMinutes = 10},
            new WorkoutProgram { Id = 2, CodeName = "INTERMEDIATE_CARDIO", Title = "Intermediate Cardio Blast", Description = "An intermediate level cardio workout program.", MainWorkoutDurationMinutes = 45 },
            new WorkoutProgram { Id = 3, CodeName = "ADVANCED_STRENGTH", Title = "Advanced Strength Training", Description = "An advanced strength training workout program.", MainWorkoutDurationMinutes = 60 }
        );
    }
}

