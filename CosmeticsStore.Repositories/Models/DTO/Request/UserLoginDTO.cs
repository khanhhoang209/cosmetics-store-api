using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class UserLoginDTO
{
    [EmailAddress(ErrorMessage = "Vui lòng nhập email!")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
    public string? Password { get; set; }
}