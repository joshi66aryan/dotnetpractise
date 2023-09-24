using System;
namespace FastFoodAPI.Models.Contact
{
	public class ContactCredentials
	{
        [Key]
        public int ContactId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Subject { get; set; }

        [MaxLength]
        public string? Message { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

