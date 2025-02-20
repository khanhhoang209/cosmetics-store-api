﻿using AutoMapper;
using CosmeticsStore.Repositories.Interfaces;
using CosmeticsStore.Repositories.Models.Domain;
using CosmeticsStore.Services.Constants;
using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Interfaces;
using CosmeticsStore.Services.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CosmeticsStore.Services.Implements;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<(ServiceResponse, string?)> RegisterAsync(UserRegisterDTO requestBody, string role)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            var user = new ApplicationUser()
            {
                UserName = requestBody.Email,
                Email = requestBody.Email,
                EmailConfirmed = role != UserConstants.CustomerRole,
                Status = true
            };

            if (role == UserConstants.CustomerRole)
            {
                var customer = requestBody as CustomerRegisterDTO;
                user.FirstName = customer?.FirstName;
                user.LastName = customer?.LastName;
                user.Address = customer?.Address;
                user.PhoneNumber = customer?.PhoneNumber;
            }

            var identityResult = await _userManager.CreateAsync(user, requestBody.Password!);
            if (!identityResult.Succeeded)
            {
                return (serviceResponse
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status409Conflict)
                        .AddDetail("message", "Tạo tài khoản thất bại!")
                        .AddError("availableUser", "Tài khoản đã tồn tại!")
                        , null);
            }

            identityResult = await _userManager.AddToRoleAsync(user, role);
            if (!identityResult.Succeeded)
            {
                return (serviceResponse
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status400BadRequest)
                        .AddDetail("message", "Tạo tài khoản thất bại!")
                        .AddError("invalidCredentials", "Vai trò yêu cầu không tồn tại!")
                        , null);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return (serviceResponse
                    .SetSucceeded(true)
                    .AddDetail("message", "Tạo mới tài khoản thành công!")
                    , token);
        }
        catch
        {
            return (serviceResponse
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status500InternalServerError)
                    .AddDetail("message", "Tạo tài khoản thất bại!")
                    .AddError("outOfService", "Không thể tạo tài khoản ngay lúc này!")
                    , null);
        }
    }

    public async Task<ServiceResponse> LoginAsync(UserLoginDTO requestBody)
    {
        var serviceResponse = new ServiceResponse();
        try
        {
            var user = await _userManager.FindByNameAsync(requestBody.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, requestBody.Password!))
            {
                return serviceResponse
                    .SetSucceeded(false)
                    .AddDetail("message", "Đăng nhập thất bại!")
                    .AddError("invalidCredentials", "Tên đăng nhập hoặc mật khẩu không chính xác!");
            }

            if (!user.Status)
            {
                return serviceResponse
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status401Unauthorized)
                    .AddDetail("message", "Đăng nhập thất bại!")
                    .AddError("invalidCredentials", "Tài khoản của bạn đã bị vô hiệu hoá, vui lòng liện hệ cửa hàng để được hỗ trợ!");
            }

            var role = (await _userManager.GetRolesAsync(user))[0];
            var accessToken = _unitOfWork.TokenRepository.GenerateJwtToken(user, role);
            var refreshToken = (await _unitOfWork.TokenRepository.GenerateRefreshToken(user)).Id;

            return new ServiceResponse()
                .SetSucceeded(true)
                .AddDetail("message", "Đăng nhập thành công!")
                .AddDetail("accessToken", accessToken)
                .AddDetail("refreshToken", refreshToken);
        }
        catch
        {
            return serviceResponse
                .SetSucceeded(false)
                .SetStatusCode(StatusCodes.Status500InternalServerError)
                .AddDetail("message", "Đăng nhập thất bại!")
                .AddError("outOfService", "Không thể đăng nhập ngay lúc này!");
        }
    }
}