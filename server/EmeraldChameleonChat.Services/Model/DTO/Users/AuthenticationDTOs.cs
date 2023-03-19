using System.ComponentModel.DataAnnotations;
using EmeraldChameleonChat.Services.Model.Entity.Users;

namespace EmeraldChameleonChat.Services.Model.DTO.Users
{
    public class RegisterDTO
    {
        public string? Name { get; set; }
        [EmailAddress, Required] public string Email { get; set; } = string.Empty;
        public string? Mobile { get; set; }

        [Required] public string Username { get; set; } = string.Empty;

        [Required] public string Password { get; set; } = string.Empty;
    }



    public class LoginDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = String.Empty;
    }

    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginResponseDTO()
        {

        }
        public LoginResponseDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
    public class TokenValidationResponseDTO
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public TokenValidationResponseDTO(User user)
        {
            User = user;
        }
    }
}
