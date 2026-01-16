using System.ComponentModel.DataAnnotations;

using WebShopApp.Models.Brand;
using WebShopApp.Models.Category;

namespace WebShopApp.Models.Product
{
    public class ProductEditVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public List<BrandPairVM> Brands { get; set; } = new();

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<CategoryPairVM> Categories { get; set; } = new();

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Range(0, 5000)]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}

