using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using System.Threading;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public interface IUserService
    {
        Task<bool> ConfirmUser(string userName, string confirmationCode, CancellationToken cancellationToken);
        Task<User> FindUser(LoginModel loginModel);
        Task<LoginResponseModel> LoginUser(User registeredUser, string providedPassword, CancellationToken cancellationToken);
        Task<LoginResponseModel> RegisterUser(RegisterModel registerModel, CancellationToken cancellationToken);
        Task SendConfirmation(User registeredUser);
    }
}
