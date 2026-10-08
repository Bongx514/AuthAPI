using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public RegistrationController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.userName) ||
                string.IsNullOrWhiteSpace(request.firstName) ||
                string.IsNullOrWhiteSpace(request.lastName) ||
                string.IsNullOrWhiteSpace(request.userEmail) ||
                string.IsNullOrWhiteSpace(request.password))
            {
                return BadRequest(new
                {
                    message = "All fields are required."
                });
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.userEmail == request.userEmail);

            if (existingUser != null)
            {
                return Conflict(new
                {
                    message = "A user with this email already exists."
                });
            }

            var user = new UsersModel
            {
                userName = request.userName,
                firstName = request.firstName,
                lastName = request.lastName,
                userEmail = request.userEmail,
                hashPassword = BCrypt.Net.BCrypt.HashPassword(request.password),
                isActive = true,
                isBlocked = false,
                dateCreated = DateTime.UtcNow,
                lastLogin = null
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Created("", new
            {
                message = "Registration successful.",
                userID = user.pkiUserID
            });
        }
    }
}