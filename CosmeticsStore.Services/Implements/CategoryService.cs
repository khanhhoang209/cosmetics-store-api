using AutoMapper;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.DTO.Response;
using CosmeticsStore.Services.Interfaces;
using CosmeticsStore.Services.Schema;
using Microsoft.AspNetCore.Http;

namespace CosmeticsStore.Services.Implements;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return serviceResponse
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status404NotFound)
                    .AddDetail("message", "Lấy thông loại mỹ phẩm thất bại!")
                    .AddError("notFound", "Không tìm thấy loại mỹ phẩm!");
            }

            var categoryDTO = _mapper.Map<CategoryResponseDTO>(category);
            return serviceResponse
                .SetSucceeded(true)
                .AddDetail("message", "Lấy thông tin loại mỹ phẩm thành công!")
                .AddDetail("data", new { category = categoryDTO });
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Lấy thông loại mỹ phẩm thất bại!")
                .AddError("outOfService", "Không thể lấy thông tin loại mỹ phẩm ngay lúc này!");
        }
    }

    public async Task<ServiceResponse> CreateAsync(CategoryCreateDTO category)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            var newCategory = _mapper.Map<Category>(category);

            var success = await _unitOfWork.CategoryRepository.CreateAsync(newCategory);
            if (!success)
            {
                return serviceResponse
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status400BadRequest)
                    .AddDetail("message", "Thêm mới loại mỹ phẩm thất bại!")
                    .AddError("invalidCredentials", "Thông tin yêu cầu chưa chính xác hoặc không hợp lệ!");
            }

            return serviceResponse
                .SetSucceeded(true)
                .AddDetail("message", "Thêm mới loại mỹ phẩm thành công!");
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Thêm mới loại mỹ phẩm thất bại!")
                .AddError("outOfService", "Không thể thêm mới loại mỹ phẩm ngay lúc này!");
        }
    }
}