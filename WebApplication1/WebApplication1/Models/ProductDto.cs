using System.ComponentModel.DataAnnotations;

namespace OdevWebApi.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }
    }
}
