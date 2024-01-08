using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Helpers;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace KintellaLocalizationREST.Services
{
    public class AuthService
    {
        private readonly TokenService _tokenService;
        AppSettings appSettings;

        public AuthService()
        {
            appSettings = new AppSettings();
            _tokenService = new TokenService(appSettings.Secret, appSettings.Issuer, appSettings.Audience, 180);
        }

        public string CreateTokenForUser(string username, string password)
        {
            User user = new();
            user.Username = username;
            user.Password = password;
            string token = _tokenService.GenerateClaimsForUser(user);
            return token;

        }
    }
}
