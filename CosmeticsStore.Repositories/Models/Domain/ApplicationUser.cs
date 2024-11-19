using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace CosmeticsStore.Repositories.Models.Domain;

public class ApplicationUser : IdentityUser
{
    [MaxLength(45)]
    public string? FirstName { get; set; }

    [MaxLength(45)]
    public string? LastName { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    public int Gender { get; set; }

    public DateTimeOffset BirthDate { get; set; }

    public bool Status { get; set; }

    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}