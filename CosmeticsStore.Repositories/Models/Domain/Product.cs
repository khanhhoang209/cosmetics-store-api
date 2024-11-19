using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("Product")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? Image { get; set; }

    public string? Describe { get; set; }

    public int CategoryId { get; set; }

    public bool? Status { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
