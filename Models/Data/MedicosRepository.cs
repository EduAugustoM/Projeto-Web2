using Dapper;
using Models.Entidades;
namespace Models.Data;

public class MedicosRepository : AbstractRepository<Medicos>
{
    private readonly DapperContext context;
    public MedicosRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public override void Atualizar(Medicos model)
    {
        string query = @"UPDATE Medicos SET
            nome = @nome, 
            idade = @idade, 
            especialidade = @especialidade,
            CPF = @CPF,
            cidade = @cidade,
            nroa = @nroa
        where codm = @codm
        ";

        using (var connection = context.CreateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }

    public override Medicos Buscar(int id)
    {
        string query = "SELECT * FROM Medicos where codm = @id";

        using (var connection = context.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Medicos>(query, new { id});
        }
    }

    public override List<Medicos> BuscarTodos()
    {
        string query = "SELECT * FROM Medicos";

        using (var connection = context.CreateConnection())
        {
            return connection.Query<Medicos>(query).ToList();
        }
    }

    public override void Excluir(Medicos model)
    {
       string query = "DELETE FROM Medicos where codm = @id";

        using (var connection = context.CreateConnection())
        {
            connection.Execute(query, new {id = model.codm });
        }
    }

    public override void Salvar(Medicos model)
    {
        string queryID = "SELECT MAX(codm) codm FROM Medicos";
        using (var connection = context.CreateConnection())
        {
            var id = connection.QueryFirstOrDefault<int>(queryID);
            model.codm = id + 1;
        }

        string query = @"INSERT INTO Medicos
            (codm, nome, idade, especialidade, CPF, cidade, nroa)
            VALUES
            (@codm, @nome, @idade, @especialidade, @CPF, @cidade, @nroa)
        ";

        using (var connection = context.CreateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }
}