using API.Domain.DTOs;
using API.Domain.Entities;
using AutoMapper;

namespace API.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<UserDTOCreateResult, UserEntity>().ReverseMap();
            CreateMap<UserDTOUpdateResult, UserEntity>().ReverseMap();

        }
    }
}