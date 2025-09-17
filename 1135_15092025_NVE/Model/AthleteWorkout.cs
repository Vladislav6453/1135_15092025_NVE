using System;
using System.Collections.Generic;

namespace _1135_15092025_NVE.Model;

public partial class AthleteWorkout
{
    public int Id { get; set; }

    public int AthleteId { get; set; }

    public int WorkoutId { get; set; }

    public int? Mark { get; set; }

    public string? Comment { get; set; }

    public virtual Athlete Athlete { get; set; } = null!;

    public virtual Workout Workout { get; set; } = null!;
}
