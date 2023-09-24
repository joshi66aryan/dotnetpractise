using System;
using FastFoodAPI.Models.Payment;
using FastFoodAPI.Models.Product;
using FastFoodAPI.Models.User;

namespace FastFoodAPI.Models.Order
{
	public class OrderItems
	{

        [Key]
        public int OrderDetailsId { get; set; }

        [MaxLength]
        public string? OrderNo { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual ProductsDetails? Product { get; set; }
        public virtual Users? User { get; set; }
        public virtual PaymentMethods? Payment { get; set; }
    }
}

