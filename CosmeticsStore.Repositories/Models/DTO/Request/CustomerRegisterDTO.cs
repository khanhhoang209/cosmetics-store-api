using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class CustomerRegisterDTO : UserRegisterDTO
{
    [Required(ErrorMessage = "Vui lòng nhập tên!", AllowEmptyStrings = false)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ!", AllowEmptyStrings = false)]
    public string? LastName { get; set; }
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có đúng 10 chữ số!")]
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}