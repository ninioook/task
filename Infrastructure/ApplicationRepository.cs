using Core.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class ApplicationRepository : IApplicationRepository
{
    private readonly string _connectionString;

    public ApplicationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task Add(Application application, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Applications (Amount, CurrencyId, StatusId, CreateDate, ActiveUntil,TypeId, CustomerId)
            VALUES (@Amount, @CurrencyId, @StatusId, SYSUTCDATETIME(), @ActiveUntil,@TypeId, @CustomerId);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var id = await connection.ExecuteScalarAsync<long>(
            new CommandDefinition(sql, application, cancellationToken: cancellationToken)
        );

        application.Id = id;
    }

    public async Task UpdateStatus(long applicationId, int statusId, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Applications
            SET StatusId = @StatusId
            WHERE Id = @ApplicationId";

        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(
            new CommandDefinition(sql, new { ApplicationId = applicationId, StatusId = statusId }, cancellationToken: cancellationToken)
        );
    }

    public async Task<IEnumerable<Application>> GetByStatusId(int statusId, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT *
            FROM Applications
            WHERE StatusId = @StatusId";

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Application>(
            new CommandDefinition(sql, new { StatusId = statusId }, cancellationToken: cancellationToken)
        );
    }
}