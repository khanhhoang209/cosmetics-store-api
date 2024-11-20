using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.DTO.Response;
using CosmeticsStore.Services.Schema;

namespace CosmeticsStore.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResponse> GetAllAsync(CategoryGetDTO category);
    Task<ServiceResponse> GetByIdAsync(int id);
    Task<ServiceResponse> CreateAsync(CategoryCreateDTO category);
    Task<ServiceResponse> UpdateAsync(CategoryUpdateDTO category);
    Task<ServiceResponse> RemoveAsync(int id);
    Task<ServiceResponse> RestoreAsync(int id);
}