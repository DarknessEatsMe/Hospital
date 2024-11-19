using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class DoctorService
{
    public int DocServId { get; set; }

    public int DocId { get; set; }

    public int ServId { get; set; }

    public int Price { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor Doc { get; set; } = null!;

    public virtual Service Serv { get; set; } = null!;
}
