using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Managers
{
    public class UserDataManager
    {
        private readonly ApplicationDbContext _context;

        public UserDataManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void StoreHashedPasswordForUserAsync(string hash, string username)
        {
            User userInDb = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (userInDb != null)
            {
                userInDb.Password = hash;
                _context.Users.Update(userInDb);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("The user was not found");
            }
        }
        public async Task<User> GetUserAsync(string username)
        {
            User user = new();
            try
            {
                // Get the user from the database
                user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
                return user;
            }
            catch (Exception ex)
            {
                
            }
            return user;
        }
        public List<User> GetAllUsers()
        {
            List<User> allUsers = null;
            try
            {
                allUsers = _context.Users.ToList();
                // Get the user from the database                
            }
            catch (Exception ex)
            {

            }
            return allUsers;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                // Save changes to the database
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException ex)
            {
                // TODO: Log the error
                // TODO: Clean up
                // TODO: Recast to a higher level for better error handling
                throw new Exception("An error occured while saving changes to the database.", ex);
            }
        }

        // TODO: måske have flere datamanger? 
        
    }
}
