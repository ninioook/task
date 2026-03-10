using Core.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<Customer> CheckUserName(string userName, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM Customers
            WHERE Name = @UserName
        ";

        return await connection.QueryFirstOrDefaultAsync<Customer>(
         sql,
         new { UserName = userName }
     );
    }

    public async Task Register(Customer customer, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        const string sql = @"INSERT INTO Customers (Name, LastName, UserName,PersonalNumber, BirthDate, Password, StatusId)
                             VALUES (@Name, @LastName,@UserName, @PersonalNumber, @BirthDate, @Password, @StatusId);
                             SELECT CAST(SCOPE_IDENTITY() as bigint);";

        await connection.OpenAsync(cancellationToken);
        var id = await connection.ExecuteScalarAsync<long?>(sql, new
        {
            customer.Name,
            customer.LastName,
            customer.UserName,
            customer.PersonalNumber,
            customer.BirthDate,
            customer.Password,
            customer.StatusId
        });

        if (id.HasValue)
        {
            customer.Id = id.Value;
        }
    }
}

