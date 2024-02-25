namespace Doggo.Infrastructure.Persistence.SqlConnectionFactory;

using System.Data;
using Application.Abstractions.SqlConnectionFactory;
using Domain.Constants;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(ConnectionConstants.Postgres) ?? throw  new Exception("Connection string is missing");
    }

    public IDbConnection CreateNpgSqlConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}