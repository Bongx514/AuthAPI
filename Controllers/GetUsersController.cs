using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUsersController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public GetUsersController(AuthDbContext context)
        {
            _context = context;
        }

        public List<vw_ActiveUsers>? ActiveUsers { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            ActiveUsers = await _context.vw_ActiveUsers
                .Where(a => a.isActive == true)
                .ToListAsync();

            if (ActiveUsers == null)
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
