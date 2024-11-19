using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("OrderDetail")]
public partial class OrderDetail
{
    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int? Quantity { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;
}
