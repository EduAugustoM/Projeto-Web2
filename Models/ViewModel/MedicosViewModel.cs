using System.ComponentModel.DataAnnotations;
namespace Models.ViewModel;

public class MedicosViewModel
{
    [Display(Name = "Codigo")]
    public int codm { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string nome { get; set; } = string.Empty;

    [Display(Name = "Idade")]
    [Required(ErrorMessage = "A idade é obrigatório")]
    public int idade { get; set; }

    [Display(Name = "Especialidade")]
    [Required(ErrorMessage = "A especialidade é obrigatório")]
    public string especialidade { get; set; } = string.Empty;

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O CPF é obrigatório")]
    public string CPF { get; set; } = string.Empty;

    [Display(Name = "Cidade")]
    [Required(ErrorMessage = "A cidade é obrigatório")]
    public string cidade { get; set; } = string.Empty;

    [Display(Name = "Nroa")]
    [Required(ErrorMessage = "O nroa é obrigatório")]
    public string nroa { get; set; } = string.Empty;
}