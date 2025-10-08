using EMSBackend.Dtos;
using EMSBackend.Interfaces;
using EMSBackend.Mappers;
using EMSBackend.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IRepository<Leave> _leaveRepo;
        private readonly IUserContextService _userContext;

        public LeaveController(IRepository<Leave> LeaveRepo, IUserContextService userContext)
        {
            _leaveRepo = LeaveRepo;
            _userContext = userContext;
        }
        [HttpPost("apply")]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveDto model)
        {
            if (model == null)
                return BadRequest("Invalid leave data.");

            // 🔹 Get EmployeeId from JWT claims via UserContextService
            var employeeId = await _userContext.GetEmployeeIdFromClaimsAsync(User);
            if (employeeId == null)
                return Unauthorized("Employee not found.");


            // Use the mapper to create a Leave entity
            var leave = model.ToLeave((int)employeeId);

            // Save it to DB via repository
            await _leaveRepo.AddAsync(leave);
            await _leaveRepo.SaveChangesAsync();

            return Ok(new { message = "Leave applied successfully"});
        }

      


    }
}
