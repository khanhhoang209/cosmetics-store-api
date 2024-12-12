using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Schema;

namespace CosmeticsStore.Services.Interfaces;

public interface IUserService
{
    Task<(ServiceResponse, string?)> RegisterAsync(UserRegisterDTO requestBody, string role);
    Task<ServiceResponse> LoginAsync(UserLoginDTO requestBody);
}