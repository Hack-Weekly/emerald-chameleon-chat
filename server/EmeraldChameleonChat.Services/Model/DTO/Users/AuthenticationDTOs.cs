using EmeraldChameleonChat.Services.Model.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Services.Model.DTO.Users
{
    public class RegisterModel
    {
        public string? Name { get; set; }
        [EmailAddress, Required] public string Email { get; set; } = string.Empty;
        public string? Mobile { get; set; }

        [Required] public string Username { get; set; } = string.Empty;

        [Required] public string Password { get; set; } = string.Empty;
    }



    public class LoginModel
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = String.Empty;
    }

    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginResponseModel()
        {

        }
        public LoginResponseModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
    public class TokenValidationResponse
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public TokenValidationResponse(User user)
        {
            User = user;
        }
    }
}
