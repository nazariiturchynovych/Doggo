namespace Doggo.Application.Abstractions.SqlConnectionFactory;

using Npgsql;

public interface IDbConnectionFactory
{
    NpgsqlConnection CreateNpgSqlConnection();
}