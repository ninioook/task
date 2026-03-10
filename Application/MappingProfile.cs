using AutoMapper;
using Domain.Entities;

namespace Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, Customer>();
        CreateMap<AddApplicationCommand, Application>();

    }
}