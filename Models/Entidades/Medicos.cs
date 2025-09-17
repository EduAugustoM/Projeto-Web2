namespace Models.Entidades;

public class Medicos
{
    public int codm { get; set; }
    public string nome { get; set; } = string.Empty;
    public int idade { get; set; }
    public string especialidade { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string cidade { get; set; } = string.Empty;
    public string nroa { get; set; } = string.Empty;
}