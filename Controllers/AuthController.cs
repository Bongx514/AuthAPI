using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AuthDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginReguest request)
        {
            if (string.IsNullOrWhiteSpace(request.userEmail) ||
                string.IsNullOrWhiteSpace(request.password))
            {
                return BadRequest(new
                {
                    message = "Email and password are required."
                });
            }

            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.userEmail == request.userEmail);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email address or Users does not exist"
                });
            }

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.password, result.hashPassword);

            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid password."
                });
            }

            if (result.isBlocked == true)
            {
                return Unauthorized(new
                {
                    message = "Your account has been blocked."
                });
            }

            if (result.isActive != true)
            {
                return Unauthorized(new
                {
                    message = "Your account is not active."
                });
            }

            var token = GenerateToken(result);

            return Ok(new
            {
                message = "Authentication successful.",
                token = token
            });

        }

        private string GenerateToken(UsersModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyValue = _configuration["JWT_SECRET"];
            var key = Encoding.ASCII.GetBytes(keyValue);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.pkiUserID.ToString()),
                    new Claim(ClaimTypes.Name, user.userName ?? ""),
                    new Claim(ClaimTypes.Email, user.userEmail ?? "")
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
