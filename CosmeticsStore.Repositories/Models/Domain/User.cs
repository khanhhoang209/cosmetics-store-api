using System;
using System.Collections.Generic;

namespace CosmeticsStore.Repositories.Models.Domain;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public int RoleId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
