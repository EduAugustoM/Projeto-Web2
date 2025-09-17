using System.Data;
using Microsoft.Data.SqlClient;

namespace Models.Data;
public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CretateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}