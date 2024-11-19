using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class Appointment
{
    public int AppId { get; set; }

    public int ClientCardId { get; set; }

    public int DocServId { get; set; }

    public DateOnly AppDate { get; set; }

    public int DiagId { get; set; }

    public string? Course { get; set; }

    public int Price {  get; set; }

    public virtual ClientsCard ClientCard { get; set; } = null!;

    public virtual Diagnosis Diag { get; set; } = null!;

    public virtual DoctorService DocServ { get; set; } = null!;
}
