

namespace Models.Enidades;

public class Pacientes
{    
    public int codp { get; set; }
    public string nome { get; set; } = string.Empty;
    public int idade { get; set; }
    public string cidade { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string doenca { get; set; } = string.Empty;
}