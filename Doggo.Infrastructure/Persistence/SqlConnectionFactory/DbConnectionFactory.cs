namespace Doggo.Infrastructure.Persistence.SqlConnectionFactory;

using Application.Abstractions.Persistence.SqlConnectionFactory;
using Domain.Constants;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NpgsqlConnection CreateNpgSqlConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString(ConnectionConstants.Postgres));
    }
}