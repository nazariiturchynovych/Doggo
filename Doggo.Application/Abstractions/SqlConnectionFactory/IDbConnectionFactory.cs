namespace Doggo.Application.Abstractions.Persistence.SqlConnectionFactory;

using Npgsql;

public interface IDbConnectionFactory
{
    NpgsqlConnection CreateNpgSqlConnection();
}