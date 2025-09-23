using AspNet_MVC.Models.Entidades;
using Dapper;
using Models.Entidades;

namespace Models.Data;

public class ConsultasRepository 
{
    private readonly DapperContext context;
    public ConsultasRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public void Atualizar(Consultas model)
    {
        var modelBD = Buscar(model.codm, model.data, model.hora);
        Excluir(modelBD);
        Salvar(modelBD);
    }

    public Consultas? Buscar(int id, DateTime date, TimeSpan hora)
    {
        string query = "SELECT * FROM Consultas where codm = @id AND data = @date AND hora = @hora";

        using (var connection = context.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Consultas>(query, new { id, date, hora});
        }
    }

    public List<Consultas> BuscarTodos()
    {
        string query = "SELECT * FROM Consultas";

        using (var connection = context.CreateConnection())
        {
            return connection.Query<Consultas>(query).ToList();
        }
    }

    public void Excluir(Consultas model)
    {
       string query = "DELETE FROM Consultas where codm = @codm AND data = @data AND hora = @hora";

        using (var connection = context.CreateConnection())
        {
            connection.Execute(query, new {model.codm, model.data, model.hora });
        }
    }

    public void Salvar(Consultas model)
    {
        string query = @"INSERT INTO Consultas(codm, codp, data, hora)
            VALUES (@codm,@codp, @data, @hora)";

        using (var connection = context.CreateConnection())
        {  
            connection.ExecuteScalar(query, model);
        }
    }
}