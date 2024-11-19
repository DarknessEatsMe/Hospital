using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class Service
{
    public int ServId { get; set; }

    public string ServName { get; set; } = null!;

    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
}
