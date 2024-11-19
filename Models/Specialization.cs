using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class Specialization
{
    public int SpecId { get; set; }

    public string SpecName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
