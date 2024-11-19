using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "Vui lòng nhập tên!")]
    public string? Name { get; set; }

    public bool? Status { get; set; }
}