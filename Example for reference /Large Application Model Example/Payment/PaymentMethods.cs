using System;
namespace FastFoodAPI.Models.Payment
{
	public class PaymentMethods
	{
        [Key]
        public int PaymentsId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        public string? CardNo { get; set; }

        [MaxLength(50)]
        public string? ExpiryDate { get; set; }

        public int? CvvNo { get; set; }

        [MaxLength]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? PaymentMode { get; set; }
    }
}

