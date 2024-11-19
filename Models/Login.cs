using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health.Models;

public partial class Login
{
    public int DocId { get; set; }
    [Required(ErrorMessage = "Это поле обязательное!")]
    public string Log { get; set; } = null!;

    [Required(ErrorMessage = "Это поле обязательное!")]
    public string Password { get; set; } = null!;

    public virtual Doctor? Doc { get; set; } 
}
