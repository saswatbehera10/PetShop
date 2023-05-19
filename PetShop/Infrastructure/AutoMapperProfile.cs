﻿using AutoMapper;
using PetShop.BusinessLogicLayer.DTO;
using PetShop.DataAccessLayer.Entities;

namespace PetShop.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Pet, PetDTO>();
            CreateMap<PetDTO, Pet>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
