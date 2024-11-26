using System.Linq.Expressions;
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
    private int _sizePerPage = 2;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Service method
    
    public async Task<ServiceResponse> GetAllAsync(CategoryGetDTO category)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            Expression<Func<Category, bool>> filter = GetFilter(category);
            var (models, totalPages) = await _unitOfWork.CategoryRepository
                .GetFilterAsync(filter, null, _sizePerPage * category.Page, _sizePerPage);

            var categories = _mapper.Map<IEnumerable<CategoryResponseDTO>>(models);
            return serviceResponse
                .SetSucceeded(true)
                .AddDetail("message", "Lấy danh sách mỹ phẩm thành công!")
                .AddDetail("data", new { totalPages, currentPage = category.Page, categories });
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Lấy danh sách mỹ phẩm thất bại!")
                .AddError("outOfService", "Không thể lấy danh sách loại mỹ phẩm ngay lúc này!");
        }
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
                    .AddDetail("message", "Lấy thông tin loại mỹ phẩm thất bại!")
                    .AddError("notFound", "Không tìm thấy loại mỹ phẩm ngay lúc này!");
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
                .AddDetail("message", "Lấy thông tin loại mỹ phẩm thất bại!")
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

            return serviceResponse
                .SetSucceeded(success)
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

    public async Task<ServiceResponse> UpdateAsync(CategoryUpdateDTO category)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            var updateCategory = _mapper.Map<Category>(category);

            var success = await _unitOfWork.CategoryRepository.UpdateAsync(updateCategory);

            return serviceResponse
                .SetSucceeded(success)
                .AddDetail("message", "Chỉnh sửa loại mỹ phẩm thành công!");
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Chỉnh sửa loại mỹ phẩm thất bại!")
                .AddError("outOfService", "Không thể chỉnh sửa loại mỹ phẩm ngay lúc này!");
        }
    }

    public async Task<ServiceResponse> RemoveAsync(int id)
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
                    .AddDetail("message", "Xóa loại mỹ phẩm thất bại!")
                    .AddError("notFound", "Không tìm thấy loại mỹ phẩm ngay lúc này!");
            }

            category.Status = false;

            var success = await _unitOfWork.CategoryRepository.UpdateAsync(category);
            return serviceResponse
                .SetSucceeded(success)
                .AddDetail("message", "Xóa loại mỹ phẩm thành công!");
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

    public async Task<ServiceResponse> RestoreAsync(int id)
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
                    .AddDetail("message", "Khôi phục loại mỹ phẩm thất bại!")
                    .AddError("notFound", "Không tìm thấy loại mỹ phẩm ngay lúc này!");
            }

            category.Status = true;

            var success = await _unitOfWork.CategoryRepository.UpdateAsync(category);
            return serviceResponse
                .SetSucceeded(success)
                .AddDetail("message", "Khôi phục loại mỹ phẩm thành công!");
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Khôi phục loại mỹ phẩm thất bại!")
                .AddError("outOfService", "Không thể khôi phục loại mỹ phẩm ngay lúc này!");
        }
    }

    #endregion

    #region Helper method

        private Expression<Func<Category, bool>> GetFilter(CategoryGetDTO category)
        {
            return (c) =>
                c.Name!.ToLower().Contains(category.Name!.ToLower() + "") &&
                c.Status == category.Status;
        }

    #endregion
}