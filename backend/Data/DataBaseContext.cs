using Npgsql;

public class DataBaseContext
{
    private readonly string _connectionString;
    public DataBaseContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}