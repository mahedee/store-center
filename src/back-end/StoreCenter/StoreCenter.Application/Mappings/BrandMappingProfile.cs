using AutoMapper;
using StoreCenter.Application.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
        }
    }
}