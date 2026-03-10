using Domain.Entities;

namespace Core.Interfaces;

public interface ICustomerRepository
{
    Task Register(Customer customer, CancellationToken cancellationToken);
    Task<Customer> CheckUserName(string userName, CancellationToken cancellationToken);
}