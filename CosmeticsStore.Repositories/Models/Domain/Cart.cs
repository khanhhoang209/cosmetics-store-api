using System;
using System.Collections.Generic;

namespace CosmeticsStore.Repositories.Models.Domain;

public partial class Cart
{
    public string UserId { get; set; } = null!;

    public int ProductId { get; set; }

    public int ProductQuantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
