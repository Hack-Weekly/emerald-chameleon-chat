using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Services.Model.Entity.Users
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        public string? Mobile { get; set; } = string.Empty;

        public string? Name { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public bool? isConfirmed { get; set; } = false;
        public string? ConfirmationCode { get; set; }
    }
}
