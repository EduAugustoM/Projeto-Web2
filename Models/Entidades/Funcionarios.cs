namespace Models.Entidades;

public class Funcionarios
{
    public int codf { get; set; }
    public string nome { get; set; } = string.Empty;
    public int idade { get; set; }
    public string cidade { get; set; } = string.Empty;
    public string salario { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}