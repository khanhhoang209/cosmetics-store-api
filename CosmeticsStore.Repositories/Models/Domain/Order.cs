using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("Order")]
public partial class Order
{
    [Key]
    public Guid OrderId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTimeOffset? CreateAt { get; set; }

    public DateTimeOffset? DelivereAt { get; set; }

    public decimal Total { get; set; }

    public string? Note { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string ShippingStatus { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual ApplicationUser User { get; set; } = null!;
}
