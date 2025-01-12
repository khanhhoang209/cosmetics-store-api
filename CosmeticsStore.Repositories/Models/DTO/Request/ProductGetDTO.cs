namespace CosmeticsStore.Services.DTO.Request;

public class ProductGetDTO
{
    public int Page { get; set; } = 0;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public long Price { get; set; }

    public int Quantity { get; set; }

    public string? Image { get; set; }

    public string? Describe { get; set; }

    public int CategoryId { get; set; }

    public bool? Status { get; set; }
}