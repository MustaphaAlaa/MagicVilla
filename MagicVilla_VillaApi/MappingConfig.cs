﻿using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, CreateVillaRequest>().ReverseMap();

            //VillaNumber

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, CreateVillaNumberRequest>().ReverseMap();
            CreateMap<VillaNumber, UpdateVillaNumber>().ReverseMap();

        }

    }
}
