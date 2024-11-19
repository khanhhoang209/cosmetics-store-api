using AutoMapper;
using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.DTO.Response;

namespace CosmeticsStore.Services.Utils;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Map for Category
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<Category, CategoryResponseDTO>();
    }
}