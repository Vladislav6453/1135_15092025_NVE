using System;
using System.Collections.Generic;

namespace _1135_15092025_NVE.Model;

public partial class LevelOfTraining
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Athlete> Athletes { get; set; } = new List<Athlete>();
}
