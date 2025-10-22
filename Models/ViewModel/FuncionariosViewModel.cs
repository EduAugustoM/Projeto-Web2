using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.ViewModel;

public class FuncionariosViewModel
{
    [Display(Name = "Codigo")]
    public int codf { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string nome { get; set; } = string.Empty;

    [Display(Name = "Idade")]
    [Required(ErrorMessage = "A idade é obrigatório")]
    public int idade { get; set; }

    [Display(Name = "Estado")]
    [Required(ErrorMessage = "O estado é obrigatório")]
    public string estado { get; set; } = string.Empty; // ✅ Novo campo estado

    [Display(Name = "Cidade")]
    [Required(ErrorMessage = "A cidade é obrigatório")]
    public string cidade { get; set; } = string.Empty;

    [Display(Name = "Salario")]
    [Required(ErrorMessage = "O salario é obrigatório")]
    public string salario { get; set; } = string.Empty;

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O CPF é obrigatório")]
    public string CPF { get; set; } = string.Empty;

    [ValidateNever]
    public List<SelectListItem> Cidades { get; set; } = new List<SelectListItem>();

    [ValidateNever]
    public List<SelectListItem> Estados { get; set; } = new List<SelectListItem>();
    [Display(Name = "Estado")]
    public string? EstadoNome { get; set; }

    [Display(Name = "Cidade")]
    public string? CidadeNome { get; set; }
}