using AutoMapper;

using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Models;

namespace MagicVilla_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, CreateVillaRequest>().ReverseMap();


            //CreateMap<VillaDTO, UpdateVilla  >().ReverseMap();

            //VillaNumber

            CreateMap<VillaNumberDTO, CreateVillaNumberRequest>().ReverseMap();
            CreateMap<VillaNumberDTO, UpdateVillaNumber>().ReverseMap();

        }

    }
}
