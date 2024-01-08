using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace KintellaLocalizationREST.Services
{
    public class UserService : IUserService
    {
        private readonly UserDataManager _userDataManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(UserDataManager userDataManager, IPasswordHasher<User> passwordHasher)
        {
            _userDataManager = userDataManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> HashIncomingPasswordAsync(string username, string password)
        {
            bool success = true;
            try
            {
                string hash = _passwordHasher.HashPassword(new User(), password);
                _userDataManager.StoreHashedPasswordForUserAsync(hash, username);
            }
            catch (Exception e)
            {
                success = false;
                throw new ArgumentException("Error while hashing \n" + e.Message);
            }
            
            return success;
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            try
            {
                var user = await _userDataManager.GetUserAsync(username);

                // Check if user was found and that the password matches
                if (user != null)
                {
                    var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        return user; // User has been validated.
                    }
                }
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            

            // Returns null if the user is not found or the password is incorrect
            return null;
        }
        public List<User> GetAllUsers()
        {
            return _userDataManager.GetAllUsers();
        }
    }
}
