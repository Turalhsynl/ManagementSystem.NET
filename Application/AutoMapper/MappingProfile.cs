using Application.CQRS.Users.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Command, User>().ReverseMap();
        CreateMap<User, RegisterDto>();

        CreateMap<User, UpdateDto>();
    }
}
