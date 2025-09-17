using System;
using System.Collections.Generic;

namespace _1135_15092025_NVE.Model;

public partial class Athlete
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public int CategoryId { get; set; }

    public int LevelId { get; set; }

    public virtual ICollection<AthleteWorkout> AthleteWorkouts { get; set; } = new List<AthleteWorkout>();

    public virtual AthletesCategory Category { get; set; } = null!;

    public virtual LevelOfTrainig Level { get; set; } = null!;
}
