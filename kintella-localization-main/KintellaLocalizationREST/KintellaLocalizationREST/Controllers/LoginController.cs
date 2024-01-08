using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KintellaLocalizationREST.Controllers
{
    [Authorize] // Requires every action in this controller to be authenticated 
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private UserDataManager _dbmanager = new(new ApplicationDbContext());
        private readonly AuthService _authService = new();

        public LoginController()
        {
            _userService = new UserService(_dbmanager, new PasswordHasher<User>());
        }
        [AllowAnonymous]
        [HttpGet("GetToken")]
        public IActionResult GetToken(string username, string password) 
        {
            Task<User> user = _userService.ValidateUserAsync(username, password);
            if(user.Result == null)
            {
                return NotFound();
            }
            string token = _authService.CreateTokenForUser(username, password);

            return Ok(token);
        }

        // GET api/<LoginController>/5
        [HttpGet("All")]
        public List<User> GetAll()
        {
            return _userService.GetAllUsers();
        }

        // POST api/<LoginController>
        [HttpPost("HashPassword")]
        public void Post(string username, string password)
        {
            // TODO 
            _userService.HashIncomingPasswordAsync(username, password);
        }
    }
}
