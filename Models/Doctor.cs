using System;
using System.Collections.Generic;

namespace Health.Models;

public partial class Doctor
{
    public int DocId { get; set; }

    public int SpecId { get; set; }

    public int PersonInfoId { get; set; }

    public int CatId { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();

    public virtual Login? Login { get; set; }

    public virtual PersonInfo PersonInfo { get; set; } = null!;

    public virtual Specialization Spec { get; set; } = null!;
}
