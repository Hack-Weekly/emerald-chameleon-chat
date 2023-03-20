using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using System.Threading;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public interface IUserService
    {
        Task<bool> ConfirmUser(string userName, string confirmationCode, CancellationToken cancellationToken);
        Task<User> FindUser(LoginDTO loginModel);
        Task<LoginResponseDTO> LoginUser(User registeredUser, string providedPassword, CancellationToken cancellationToken);
        Task<LoginResponseDTO> RegisterUser(RegisterDTO registerModel, CancellationToken cancellationToken);
        Task SendConfirmation(User registeredUser);
    }
}
