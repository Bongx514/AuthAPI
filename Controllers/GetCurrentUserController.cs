using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetCurrentUserController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public GetCurrentUserController(AuthDbContext context)
        {
            _context = context;
        }

        public UsersModel? CurrentUser { get; set; } = new UsersModel();

        [HttpGet("{userid}")]
        public async Task<IActionResult> GetCurrentUserAsync(int userid)
        {
            CurrentUser = await _context.Users
                 .Where(u => u.pkiUserID == userid)
                 .FirstOrDefaultAsync();

            if (CurrentUser == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            return Ok(CurrentUser);
        }
    }
}
