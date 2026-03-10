using AutoMapper;

namespace Core;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, Customer>();

    }
}