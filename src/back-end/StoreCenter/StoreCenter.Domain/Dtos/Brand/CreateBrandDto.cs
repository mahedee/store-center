using System.ComponentModel.DataAnnotations;

namespace StoreCenter.Application.DTOs.Brand
{
    public class CreateBrandDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Url]
        public string? Website { get; set; }

        [Url]
        public string? LogoUrl { get; set; }
    }
}