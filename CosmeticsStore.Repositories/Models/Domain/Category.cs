using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CosmeticsStore.Repositories.Models.Domain;

[Table("Category")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public bool? Status { get; set; }

    [JsonIgnore]
    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
