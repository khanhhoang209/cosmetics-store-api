namespace CosmeticsStore.Services.DTO.Request;

public class CategoryGetDTO
{
    public int Page { get; set; } = 0;

    public string? Name { get; set; } = string.Empty;

    public bool Status { get; set; } = true;
}