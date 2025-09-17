using System;
using System.Collections.Generic;

namespace _1135_15092025_NVE.Model;

public partial class Workout
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int Duration { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<AthleteWorkout> AthleteWorkouts { get; set; } = new List<AthleteWorkout>();

    public virtual WorkoutType Type { get; set; } = null!;
}
