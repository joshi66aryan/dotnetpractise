using System;
namespace FastFoodAPI.Models.User
{
	public class Users
	{
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string? Username { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; }

        [MaxLength(50)]
        [Required]
        public string? Email { get; set; }

        [MaxLength]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? PostCode { get; set; }

        [MaxLength]
        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

