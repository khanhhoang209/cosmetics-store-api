using System;
using System.Collections.Generic;

namespace CosmeticsStore.Repositories.Models.Domain;

public partial class Order
{
    public Guid OrderId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTimeOffset? CreateAt { get; set; }

    public DateTimeOffset? DelivereAt { get; set; }

    public decimal Total { get; set; }

    public string? Note { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string ShippingStatus { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
