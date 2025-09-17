
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class PacientesViewModel
{
    [Display(Name = "Codigo")]
    public int codp { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string nome { get; set; } = string.Empty;

    [Display(Name = "Idade")]
    [Required(ErrorMessage = "A idade é obrigatório")]
    public int idade { get; set; }

    [Display(Name = "Cidade")]
    [Required(ErrorMessage = "A cidade é obrigatório")]
    public string cidade { get; set; } = string.Empty;

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O CPF é obrigatório")]
    public string CPF { get; set; } = string.Empty;

    [Display(Name = "Doença")]
    [Required(ErrorMessage = "A doença é obrigatório")]
    public string doenca { get; set; } = string.Empty;
}