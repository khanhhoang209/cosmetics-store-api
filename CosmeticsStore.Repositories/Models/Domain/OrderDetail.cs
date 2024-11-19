﻿using System;
using System.Collections.Generic;

namespace CosmeticsStore.Repositories.Models.Domain;

public partial class OrderDetail
{
    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}