using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> GenerateJWToken(User registeredUser, CancellationToken cancellationToken, bool refreshExpired = false);
        Task<LoginResponseDTO> ValidateRefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken);

    }
}
