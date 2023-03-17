using AutoMapper;
using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using EmeraldChameleonChat.Services.Repository;
using EmeraldChameleonChat.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _registeredUserRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        private readonly string Access = "Access";
        private readonly string Refresh = "Refresh";
        public AuthenticationService(IConfiguration configuration, IUserRepository registeredUserRepository,
                                        IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _config = configuration;
            _registeredUserRepository = registeredUserRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseModel> GenerateJWToken(User registeredUser, CancellationToken cancellationToken, bool refreshExpired = false)
        {
            LoginResponseModel loginResponseModel = new LoginResponseModel();
            var claims = GenerateClaims(registeredUser);
            loginResponseModel.AccessToken = GenerateAccessToken(claims);

            if (registeredUser.RefreshToken == null || registeredUser.RefreshTokenExpiryTime < DateTime.UtcNow || refreshExpired == true)
                loginResponseModel.RefreshToken = await GenerateRefreshToken(registeredUser, claims, cancellationToken);

            return loginResponseModel;
        }
        private List<Claim> GenerateClaims(User registeredUser)
        {

            return new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, registeredUser.Id.ToString()),
            new Claim("Username", registeredUser.Username),
            //new Claim(ClaimTypes.Role, (registeredUser.UserGroups as UserGroup)?.Role?.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"] ?? ""),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString(), ClaimValueTypes.Integer)
        };
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = _config["Jwt:Keys:Access"];
            var expiry = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:Expiry:Access"] ?? "0"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                null,
                claims,
                expires: expiry,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<string> GenerateRefreshToken(User registeredUser, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var key = _config["Jwt:Keys:Refresh"];
            var expiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:Expiry:Refresh"] ?? "0"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                null,
                claims,
                expires: expiry,
                signingCredentials: credentials);

            var returnedToken = new JwtSecurityTokenHandler().WriteToken(token);

            registeredUser.RefreshToken = Convert.ToBase64String(Util.HashPassword(returnedToken, RandomNumberGenerator.Create()));
            registeredUser.RefreshTokenExpiryTime = expiry;
            await _registeredUserRepository.UpdateAsync(registeredUser, cancellationToken, true);

            return returnedToken;
        }
        public async Task<LoginResponseModel> ValidateRefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken)
        {
            var claims = ValidateJWTToken(accessToken, Access);
            if (claims == null) return null;

            var username = _httpContextAccessor.HttpContext.User.FindFirstValue("Username");
            var user = await _registeredUserRepository.GetUserByUserName(username) ?? throw new UnauthorizedAccessException();

            //if (user.RefreshTokenExpiryTime <= DateTime.UtcNow) return null;
            if (!Util.VerifyPassword(user.RefreshToken, refreshToken)) return null;

            return await GenerateJWToken(user, cancellationToken, true);
        }
        private IEnumerable<Claim> ValidateJWTToken(string token, string tokenType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenType == Access ? _config["Jwt:Keys:Access"]
                                                                    : _config["Jwt:Keys:Refresh"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = _config["Jwt:Issuer"],
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims;
            }
            catch
            {
                // return null if validation fails
                throw;
            }
        }

    }
}

