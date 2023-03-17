using AutoMapper;
using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using EmeraldChameleonChat.Services.Repository;
using EmeraldChameleonChat.Services.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _registeredUserRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public const string ConfirmationURL = "MailSettings:ConfirmationURL";

        public UserService(IUserRepository registeredUserRepository, IAuthenticationService authenticationService,
                           IMapper mapper, IWebHostEnvironment webHostEnvironment, IEmailService emailService, IConfiguration configuration)
        {
            _registeredUserRepository = registeredUserRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _registeredUserRepository.UserExists(username, email);
        }
        public async Task<User> FindUser(LoginModel loginModel)
        {
            if (loginModel.Username != null)
                return await _registeredUserRepository.GetUserByUserName(loginModel.Username);
            if (loginModel.Email != null)
                return await _registeredUserRepository.GetUserByEmail(loginModel.Email);

            return null;
        }
        public async Task<LoginResponseModel> LoginUser(User registeredUser, string providedPassword, CancellationToken cancellationToken)
        {
            if (!Util.VerifyPassword(registeredUser.Password, providedPassword))
                return null;

            return await _authenticationService.GenerateJWToken(registeredUser, cancellationToken, true);
        }
        public async Task<LoginResponseModel> RegisterUser(RegisterModel registerModel, CancellationToken cancellationToken)
        {
            try
            {
                if (await UserExists(registerModel.Username, registerModel.Email)) throw new Exception("User Already Exists");


                var registeredUser = new User();
                registeredUser = _mapper.Map<User>(registerModel);
                registeredUser.Password = Convert.ToBase64String(Util.HashPassword(registeredUser.Password, RandomNumberGenerator.Create()));
                registeredUser.ConfirmationCode = Util.GenerateRandomPassword(15);
                var user = await _registeredUserRepository.CreateAsync(registeredUser, cancellationToken, true);

                if (user == null) return null;
                if (user.Email != null) await SendConfirmation(user);
                return await _authenticationService.GenerateJWToken(user, cancellationToken, true);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendConfirmation(User registeredUser)
        {
            string confirmationURL = _configuration[ConfirmationURL] ?? "https://localhost:7064/api/Users/Confirmation";
            try
            {
                string FilePath = "EmailTemplates/" + Path.DirectorySeparatorChar.ToString() + "VerifcationEmail.html";
                var body = Util.GetFile(FilePath, _webHostEnvironment);
                body = body.Replace("{Name}", registeredUser.Name);
                body = body.Replace("{ConfirmationCode}", registeredUser.ConfirmationCode);
                body = body.Replace("{ConfirmationURL}", $"{confirmationURL}?confirmationCode={HttpUtility.UrlEncode(registeredUser.ConfirmationCode)}&userName={registeredUser.Username}");

                await _emailService.SendEmailAsync("Confirmation Code", registeredUser.Email, body);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> ConfirmUser(string userName, string confirmationCode, CancellationToken cancellationToken)
        {
            var user = await _registeredUserRepository.GetUserByUserName(userName.ToUpper());
            if (user == null) return false;
            if (user.ConfirmationCode == confirmationCode)
            {
                user.isConfirmed = true;
                await _registeredUserRepository.UpdateAsync(user, cancellationToken, true);
            };
            return (bool)user.isConfirmed;
        }

    }
}
