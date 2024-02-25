namespace Doggo.Application.Abstractions.SqlConnectionFactory;

using System.Data;
using Npgsql;

public interface IDbConnectionFactory
{
    IDbConnection CreateNpgSqlConnection();
}