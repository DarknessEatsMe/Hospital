using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health.Models;

public partial class PersonInfo
{
    public int PersonInfoId { get; set; }
    [Required(ErrorMessage = "Это поле обязательное!")]
    public string FName { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string SName { get; set; } = null!;

    public string? FatherName { get; set; }

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string PassNum { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string PassSeries { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное!")]
    public DateOnly Birthday { get; set; }

    [Required(ErrorMessage = "Это поле обязательное!")]
    public bool Sex { get; set; }

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string Adress { get; set; } = null!;

    public byte[]? Photo { get; set; }

    [Range(0, 10, ErrorMessage ="Скидка не может быть больше 10%")]
    public byte? Discount { get; set; }

    public virtual ClientsCard? ClientsCard { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
