using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly List<User> _users = new()
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "manager", Password = "manager123", Role = "Manager" },
            new User { Username = "viewer", Password = "viewer123", Role = "Viewer" }
        };
        [HttpPost("login")]
        public IActionResult Login(User loginData)
        {
            var existingUser = _users.FirstOrDefault(u =>
                u.Username == loginData.Username &&
                u.Password == loginData.Password);

            if (existingUser == null)
                return Unauthorized("Invalid username or password");

            var jwtToken = GenerateJwtToken(existingUser);

            return Ok(new { token = jwtToken });
        }
        private string GenerateJwtToken(User user)
        {
            var jwtConfig = _configuration.GetSection("Jwt");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]!)
            );
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(jwtConfig["DurationInMinutes"])
                ),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
