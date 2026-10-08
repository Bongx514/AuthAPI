using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GetUsersController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public GetUsersController(AuthDbContext context)
        {
            _context = context;
        }

        public List<UsersModel> ActiveUsers { get; set; } = new List<UsersModel>();

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            ActiveUsers = await _context.Users
                .Where(au => au.isActive != false)
                .ToListAsync();

            if (!ActiveUsers.Any())
            {
                return Ok(new
                {
                    message = "No active users found"
                });
            }

            return Ok(ActiveUsers);
        }
    }
}
