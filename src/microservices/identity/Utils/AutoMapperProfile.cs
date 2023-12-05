using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using identityapi.DTOs;
using identityapi.Entities;

namespace identityapi.utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserDTO>();
        }

    }
}