using System;
using FastFoodAPI.Models.Category;

namespace FastFoodAPI.Models.Product
{
	public class ProductsDetails
	{
        [Key]
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [MaxLength]
        public string? ImageUrl { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CategoryItems? Category { get; set; }
    }
}




