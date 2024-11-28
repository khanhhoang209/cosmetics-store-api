using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class UserRegisterDTO
{
    [EmailAddress(ErrorMessage = "vui lòng nhập email!")]
    public string? Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập mật khẩu!")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Mật khẩu phải dài ít nhất 8 ký tự và bao gồm một chữ cái viết hoa, một chữ cái viết thường, một chữ số và một ký tự đặc biệt!")]
    public string? Password { get; set; }
}