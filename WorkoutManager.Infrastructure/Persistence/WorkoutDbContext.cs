using Microsoft.EntityFrameworkCore;

namespace WorkoutManager.Data;

public class WorkoutDbContext : DbContext
{
    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
    {
    }

    public DbSet<Models.WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<Models.ExerciseGroup> ExerciseGroups { get; set; }
    public DbSet<Models.Exercise> Exercises { get; set; }
    public DbSet<Models.Equipment> Equipment { get; set; }
    public DbSet<Models.EquipmentCategory> EquipmentCategories { get; set; }
    public DbSet<Models.Contraindication> Contraindications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure WorkoutProgram
        modelBuilder.Entity<Models.WorkoutProgram>(entity =>
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
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure ExerciseGroup
        modelBuilder.Entity<Models.ExerciseGroup>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Exercise
        modelBuilder.Entity<Models.Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Unit).IsRequired().HasMaxLength(20);

            // Many-to-One: Exercise -> ExerciseGroup
            entity.HasMany(e => e.ExerciseGroups)
                .WithMany(eq => eq.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseExerciseGroups"));

            // Many-to-Many: Exercise <-> Contraindication
            entity.HasMany(e => e.Contraindications)
                .WithMany(c => c.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseContraindications"));
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Equipment
        modelBuilder.Entity<Models.Equipment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(e => e.EquipmentCategory)
                .WithMany(ec => ec.Equipment)
                .HasForeignKey(e => e.EquipmentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many: Equipment <-> Contraindication
            entity.HasMany(e => e.Contraindications)
                .WithMany(c => c.Equipment)
                .UsingEntity(j => j.ToTable("EquipmentContraindications"));
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure EquipmentCategory
        modelBuilder.Entity<Models.EquipmentCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Contraindication
        modelBuilder.Entity<Models.Contraindication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
    }
}

