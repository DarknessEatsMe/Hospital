using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health.Models;

public partial class ClientsCard
{
    public int ClientCardId { get; set; }

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string CardNum { get; set; } = null!;

    public int PersonInfoId { get; set; }

    public DateOnly CreationDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual PersonInfo PersonInfo { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
