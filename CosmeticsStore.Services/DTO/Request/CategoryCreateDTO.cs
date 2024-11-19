using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "Vui lòng nhập tên!")]
    public string? Name { get; set; }

    [Required]
    public bool? Status { get; set; } = true;
}