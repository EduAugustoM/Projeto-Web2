using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.ViewModel;

public class PacientesViewModel
{
    [Display(Name = "Codigo")]
    public int codp { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string nome { get; set; } = string.Empty;

    [Display(Name = "Idade")]
    [Required(ErrorMessage = "A idade é obrigatória")]
    public int idade { get; set; }

    [Display(Name = "Cidade")]
    [Required(ErrorMessage = "A cidade é obrigatória")]
    public string cidade { get; set; } = string.Empty;

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "O CPF é obrigatório")]
    public string CPF { get; set; } = string.Empty;

    [Display(Name = "Doença")]
    [Required(ErrorMessage = "A doença é obrigatória")]
    public string doenca { get; set; } = string.Empty;

    [Display(Name = "Imagem")]
    public IFormFile? ImagemArquivo { get; set; }
    public string? imagem { get; set; }
}