using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Это поле обязательно!")]
    public string FName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательно!")]
    public string SName { get; set; } = null!;

    public string? FatherName { get; set; }
    [Required(ErrorMessage = "Это поле обязательно!")]
    public DateOnly Birthday { get; set; }

    [Required(ErrorMessage = "Это поле обязательно!")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Номер паспорта состоит из 4-х символов!")]
    public string PassNum { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательно!")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Серия паспорта состоит из 6-х символов!")]
    public string PassSeries { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    [Required(ErrorMessage = "Это поле обязательно!")]
    public DateTime AppDate { get; set; }

    [Required(ErrorMessage = "Это поле обязательно!")]
    public int SpecId { get; set; }

    public int? ClientCardId { get; set; }

    public virtual ClientsCard? ClientCard { get; set; }

    public virtual Specialization? Spec { get; set; }
}
