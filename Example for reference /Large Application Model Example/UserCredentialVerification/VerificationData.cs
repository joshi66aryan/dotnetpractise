using System;
namespace FastFoodAPI.Models.UserCredentialVerification
{
	public class VerificationData
	{

        [Key]
        public int VerificationDataId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        [MaxLength]
        public string? VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        [MaxLength]
        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

