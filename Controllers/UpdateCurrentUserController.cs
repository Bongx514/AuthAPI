using AuthAPI.Data;
using AuthAPI.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateCurrentUserController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public UpdateCurrentUserController(AuthDbContext context)
        {
            _context = context;
        }

        //public UsersModel? CurrentUser { get; set; }
        
        [HttpPost("{userid:int}")]
        public async Task<IActionResult> UpdateCurrentUser(int userid, [FromBody] UpdateUserRequest updatedUser)
        {
            var loggedInUser = await _context.Users
                .Where(u => u.pkiUserID == userid)
                .FirstOrDefaultAsync();

            if (loggedInUser == null)
            {
                return BadRequest("User not found");
            }

            loggedInUser?.userName = updatedUser?.userName;
            loggedInUser?.firstName = updatedUser?.firstName;
            loggedInUser?.lastName = updatedUser?.lastName;
            loggedInUser?.fkiRoleID = updatedUser?.fkiRoleID;

            await _context.SaveChangesAsync();

            return Created("", new
            {
                Message = "User updated successfully"
            });
        }
    }
}
