using System.Runtime.CompilerServices;
using Core;

namespace Infrastructure;

public class CustomerRepository:ICustomerRepository
{
    public Task Register(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}