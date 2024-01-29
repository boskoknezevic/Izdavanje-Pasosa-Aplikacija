using System.ComponentModel.DataAnnotations;

public class RegistracijaModel
{
    [Required(ErrorMessage = "ЈМБГ је обавезан.")]
    [StringLength(13, ErrorMessage = "ЈМБГ не сме бити дужи од 13 бројева.")]
    [RegularExpression(@"^[0-9]{13}$")]
    public string JMBG { get; set; }

    [Required(ErrorMessage = "Име је обавезно.")]
    [StringLength(40, ErrorMessage = "Име не сме бити дуже од 40 карактера.")]
    public string Ime { get; set; }

    [Required(ErrorMessage = "Презиме је обавезно.")]
    [StringLength(40, ErrorMessage = "Презиме не сме бити дуже од 40 карактера.")]
    public string Prezime { get; set; }


    [Required(ErrorMessage = "Имејл адреса је обавезна.")]
    [EmailAddress(ErrorMessage = "Неисправна имејл адреса.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Лозинка је обавезна.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; }

    [Required(ErrorMessage = "Држављанство је обавезно.")]
    [StringLength(40, ErrorMessage = "Држављанство не сме бити дуже од 40 карактера.")]
    public string Drzavljanstvo { get; set; }
}
