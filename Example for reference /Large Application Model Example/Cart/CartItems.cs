using System;
using FastFoodAPI.Models.Product;
using FastFoodAPI.Models.User;

namespace FastFoodAPI.Models.Cart
{
	public class CartItems
	{
        [Key]
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public virtual ProductsDetails? Product { get; set; }
        public virtual Users? User { get; set; }
    }
}




