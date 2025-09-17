using Dapper;
using Models.Entidades;
namespace Models.Data;

public class FuncionariosRepository : AbstractRepository<Funcionarios>
{
    private readonly DapperContext context;
    public FuncionariosRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public override void Atualizar(Funcionarios model)
    {
        string query = @"UPDATE Funcionarios SET
            nome = @nome, 
            idade = @idade, 
            cidade = @cidade,
            salario = @salario,
            CPF = @CPF
        where codf = @codf
        ";

        using (var connection = context.CreateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }

    public override Funcionarios Buscar(int id)
    {
        string query = "SELECT * FROM Funcionarios where codf = @id";

        using (var connection = context.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Funcionarios>(query, new { id});
        }
    }

    public override List<Funcionarios> BuscarTodos()
    {
        string query = "SELECT * FROM Funcionarios";

        using (var connection = context.CreateConnection())
        {
            return connection.Query<Funcionarios>(query).ToList();
        }
    }

    public override void Excluir(Funcionarios model)
    {
       string query = "DELETE FROM Funcionarios where codf = @id";

        using (var connection = context.CreateConnection())
        {
            connection.Execute(query, new {id = model.codf });
        }
    }

    public override void Salvar(Funcionarios model)
    {
        string query = @"INSERT INTO Funcionarios
            (nome, idade, cidade, salario, CPF)
            VALUES
            (@nome, @idade, @cidade, @salario, @CPF)";

        using (var connection = context.CreateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }
}