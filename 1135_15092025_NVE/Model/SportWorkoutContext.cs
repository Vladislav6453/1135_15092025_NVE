using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace _1135_15092025_NVE.Model;

public partial class SportWorkoutContext : DbContext
{
    public SportWorkoutContext()
    {
    }

    public SportWorkoutContext(DbContextOptions<SportWorkoutContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Athlete> Athletes { get; set; }

    public virtual DbSet<AthleteWorkout> AthleteWorkouts { get; set; }

    public virtual DbSet<AthletesCategory> AthletesCategories { get; set; }

    public virtual DbSet<LevelOfTraining> LevelOfTrainings { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutType> WorkoutTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("userid=student;password=student;database=SportWorkout;server=192.168.200.13", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Athlete>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "FK_Athletes_Athletes_Category_id");

            entity.HasIndex(e => e.LevelOfTrainingId, "FK_Athletes_LevelOfTraining_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("Category_id");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.LevelOfTrainingId)
                .HasColumnType("int(11)")
                .HasColumnName("LevelOfTraining_id");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Athletes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Athletes_Athletes_Category_id");

            entity.HasOne(d => d.LevelOfTraining).WithMany(p => p.Athletes)
                .HasForeignKey(d => d.LevelOfTrainingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Athletes_LevelOfTraining_id");
        });

        modelBuilder.Entity<AthleteWorkout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Athlete_Workouts");

            entity.HasIndex(e => e.AthleteId, "FK_Athlete_Workouts_Athletes_id");

            entity.HasIndex(e => e.WorkoutId, "FK_Athlete_Workouts_Workouts_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AthleteId)
                .HasColumnType("int(11)")
                .HasColumnName("Athlete_id");
            entity.Property(e => e.Comment).HasColumnType("tinytext");
            entity.Property(e => e.Mark).HasColumnType("int(1)");
            entity.Property(e => e.WorkoutId)
                .HasColumnType("int(11)")
                .HasColumnName("Workout_id");

            entity.HasOne(d => d.Athlete).WithMany(p => p.AthleteWorkouts)
                .HasForeignKey(d => d.AthleteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Athlete_Workouts_Athletes_id");

            entity.HasOne(d => d.Workout).WithMany(p => p.AthleteWorkouts)
                .HasForeignKey(d => d.WorkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Athlete_Workouts_Workouts_id");
        });

        modelBuilder.Entity<AthletesCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Athletes_Category");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<LevelOfTraining>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("LevelOfTraining");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.TypeId, "FK_Workouts_Type_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Duration).HasColumnType("int(11)");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("Type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Workouts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workouts_Type_id");
        });

        modelBuilder.Entity<WorkoutType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WorkoutType");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
