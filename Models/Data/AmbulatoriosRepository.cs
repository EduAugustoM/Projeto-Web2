using AspNet_MVC.Models.Entidades;
using Dapper;
using Models.Entidades;

namespace Models.Data;

public class AmbulatoriosRepository 
{
    private readonly DapperContext context;
    public AmbulatoriosRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public void Atualizar(Ambulatorios model)
    {
        var modelBD = Buscar(model.nroa);
        Excluir(modelBD);
        Salvar(modelBD);
    }

    public Ambulatorios? Buscar(int id)
    {
        string query = "SELECT * FROM Ambulatorios where nroa = @id";

        using (var connection = context.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Ambulatorios>(query, new { id });
        }
    }

    public List<Ambulatorios> BuscarTodos()
    {
        string query = "SELECT * FROM Ambulatorios";

        using (var connection = context.CreateConnection())
        {
            return connection.Query<Ambulatorios>(query).ToList();
        }
    }

    public void Excluir(Ambulatorios model)
    {
       string query = "DELETE FROM Ambulatorios where nroa = @nroa";

        using (var connection = context.CreateConnection())
        {
            connection.Execute(query, new {model.nroa });
        }
    }

    public void Salvar(Ambulatorios model)
    {
        string query = @"INSERT INTO Ambulatorios(nroa, andar, capacidade)
            VALUES (@nroa,@andar, @capacidade)";

        using (var connection = context.CreateConnection())
        {  
            connection.ExecuteScalar(query, model);
        }
    }
}