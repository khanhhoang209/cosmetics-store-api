using System;
using System.Collections.Generic;

namespace CosmeticsStore.Repositories.Models.Domain;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? Image { get; set; }

    public string? Describe { get; set; }

    public int CategoryId { get; set; }

    public bool? Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
