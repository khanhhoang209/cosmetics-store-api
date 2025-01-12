using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Interfaces;
using CosmeticsStore.Services.Schema;

namespace CosmeticsStore.Services.Implements;

public class ProductService : IProductService
{
    public Task<ServiceResponse> GetAllAsync(ProductGetDTO product)
    {
        throw new NotImplementedException();
    }
}