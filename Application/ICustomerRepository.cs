namespace Core;

public interface ICustomerRepository
{
    Task Register(Customer customer, CancellationToken cancellationToken);
}