using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("Cart")]
public partial class Cart
{
    public string UserId { get; set; } = null!;

    public int ProductId { get; set; }

    public int ProductQuantity { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Carts")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Carts")]
    public virtual ApplicationUser User { get; set; } = null!;
}
