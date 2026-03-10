using Core.Interfaces;
using Domain.Entities;

namespace Core.QueryHandlers
{
    public class CustomerQueryHandler
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> Handle(CheckUserNameQuery query, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.CheckUserName(query.UserName, cancellationToken);

            if (customer == null)
                return null;

            bool valid = PasswordHelper.VerifyPassword(query.Password, customer.Password ?? "");
            if (!valid)
                return null;

            return customer;
        }
    }
}