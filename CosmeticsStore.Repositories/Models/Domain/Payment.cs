using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("Payment")]
public partial class Payment
{
    [Key]
    public Guid Id { get; set; }

    public int MethodId { get; set; }

    public Guid OrderId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public long Amount { get; set; }

    public bool Status { get; set; }

    [ForeignKey("MethodId")]
    [InverseProperty("Payments")]
    public virtual Method Method { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("Payment")]
    public virtual Order? Order { get; set; }
}