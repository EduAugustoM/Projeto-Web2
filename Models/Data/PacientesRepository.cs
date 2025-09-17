using Dapper;
using Models.Enidades;

namespace Models.Data;

public class PacientesRepository : AbstractRepository<Pacientes>
{
    private readonly DapperContext context;
    public PacientesRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public override void Atualizar(Pacientes model)
    {
        string query = @"UPDATE Pacientes SET
            nome = @nome, 
            idade = @idade, 
            CPF = @cpf, 
            doenca = @doenca
        where codp = @codp
        ";

        using (var connection = context.CretateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }

    public override Pacientes Buscar(int id)
    {
        string query = "SELECT * FROM Pacientes where codp = @id";

        using (var connection = context.CretateConnection())
        {
            return connection.QueryFirstOrDefault<Pacientes>(query, new { id});
        }
    }

    public override List<Pacientes> BuscarTodos()
    {
        string query = "SELECT * FROM Pacientes";

        using (var connection = context.CretateConnection())
        {
            return connection.Query<Pacientes>(query).ToList();
        }
    }

    public override void Excluir(Pacientes model)
    {
       string query = "DELETE FROM Pacientes where codp = @id";

        using (var connection = context.CretateConnection())
        {
            connection.Execute(query, new {id = model.codp });
        }
    }

    public override void Salvar(Pacientes model)
    {
        string queryID = "SELECT MAX(codp) codp FROM Pacientes";

        string query = @"INSERT INTO Pacientes(codp, nome, idade, CPF, cidade, doenca)
            VALUES (@codp, @nome, @idade, @CPF, @cidade, @doenca)";

        using (var connection = context.CretateConnection())
        {
            var maxID = connection.QueryFirst<int>(queryID);
            model.codp = maxID + 1;
            connection.ExecuteScalar(query, model);
        }
    }
}