using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Schema;

namespace CosmeticsStore.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResponse> GetByIdAsync(int id);
    Task<ServiceResponse> CreateAsync(CategoryCreateDTO category);

}