using EMSBackend.Dtos;
using EMSBackend.Interfaces;
using EMSBackend.Mappers;
using EMSBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IRepository<Leave> _leaveRepo;

        public LeaveController(IRepository<Leave> LeaveRepo)
        {
            _leaveRepo = LeaveRepo;
        }
        [HttpPost("apply")]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveDto model)
        {
            if (model == null)
                return BadRequest("Invalid leave data.");

            // Use the mapper to create a Leave entity
            var leave = model.ToLeave();

            // Save it to DB via repository
            await _leaveRepo.AddAsync(leave);
            await _leaveRepo.SaveChangesAsync();

            return Ok(new { message = "Leave applied successfully"});
        }

    }
}
