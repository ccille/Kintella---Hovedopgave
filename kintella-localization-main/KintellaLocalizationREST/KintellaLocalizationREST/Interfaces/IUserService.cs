using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface IUserService
    {
        public Task<User> ValidateUserAsync(string username, string password);
        public List<User> GetAllUsers();
        public Task<bool> HashIncomingPasswordAsync(string username, string password);
    }
}
