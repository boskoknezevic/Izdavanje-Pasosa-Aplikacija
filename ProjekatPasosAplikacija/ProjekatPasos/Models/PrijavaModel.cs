using System.ComponentModel.DataAnnotations;

public class PrijavaModel
{
    [Required(ErrorMessage = "Унесите имејл.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Унесите лозинку.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; }
}
