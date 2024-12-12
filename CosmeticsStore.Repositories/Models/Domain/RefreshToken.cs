using System.ComponentModel.DataAnnotations.Schema;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("RefreshToken")]
public class RefreshToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateTimeOffset ExpirationTime { get; set; }
}