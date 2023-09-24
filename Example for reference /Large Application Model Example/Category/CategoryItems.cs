using System;
namespace FastFoodAPI.Models.Category
{
	public class CategoryItems
	{
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength]
        public string? ImageUrl { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

