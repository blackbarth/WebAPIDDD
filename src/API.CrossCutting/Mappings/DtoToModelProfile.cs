using API.Domain.DTOs;
using API.Domain.Models;
using AutoMapper;

namespace API.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<UserModel, UserDTOCreate>().ReverseMap();
            CreateMap<UserModel, UserDTOUpdate>().ReverseMap();
        }
    }
}