using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Schema;

namespace CosmeticsStore.Services.Interfaces;

public interface IProductService
{
    Task<ServiceResponse> GetAllAsync(ProductGetDTO product);
}