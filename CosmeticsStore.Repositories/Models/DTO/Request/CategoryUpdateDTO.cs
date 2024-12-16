using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Services.DTO.Request;

public class CategoryUpdateDTO
{
    [Required(ErrorMessage = "Vui lòng nhập ID!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên!")]
    public string? Name { get; set; }

    [Required]
    public bool? Status { get; set; } = true;
}