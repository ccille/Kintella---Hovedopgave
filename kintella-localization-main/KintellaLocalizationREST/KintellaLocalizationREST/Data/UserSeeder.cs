using KintellaLocalizationREST.Model;
using Microsoft.AspNetCore.Identity;

public class UserSeeder // Helper class to create a user seed for the database
{
    // Takes in a password hasher to hash the password
    private readonly IPasswordHasher<User> _passwordHasher;

    // Constructor to set the password hasher to the one passed in
    public UserSeeder(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    // Creates a user seed with the given parameters
    public User CreateUserSeed(int id, string username, string password)
    {
        var user = new User { UserID = id, Username = username };
        user.Password = _passwordHasher.HashPassword(user, password);
        return user;
    }
}
