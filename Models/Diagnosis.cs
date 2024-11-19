using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class Diagnosis
{
    public int DiagId { get; set; }

    public string DiagName { get; set; } = null!;

    public string MkbCode { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
