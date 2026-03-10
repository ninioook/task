using AutoMapper;
using Core;

namespace WebApplication2
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerModel, RegisterCommand>();
            CreateMap<ApplicationModel, AddApplicationCommand>();
        }
    }
}
