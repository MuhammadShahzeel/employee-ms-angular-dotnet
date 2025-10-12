using EMSBackend.Dtos;
using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Mappers;
using EMSBackend.Models;
using EMSBackend.Repositories;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepo;
        private readonly IUserContextService _userContext;

        public LeaveController(ILeaveRepository LeaveRepo, IUserContextService userContext)
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


        [HttpPost("update-leave")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> UpdateLeaveStatus([FromBody] LeaveDto model)
        {
            if (model == null)
                return BadRequest("Invalid leave data.");

            var leave = await _leaveRepo.GetByIdAsync(model.Id!.Value);
            if (leave == null)
                return NotFound("Leave not found.");

            //  Check role from JWT claim
            var isAdmin = _userContext.IsAdmin(User);

            if (isAdmin)
            {
                //  Admin can update any status (use mapper directly)
                leave.UpdateLeaveFromDto(model);
            }
            else
            {
                //  Employee can only cancel their leave
                if (model.Status == (int)LeaveStatus.Cancelled)
                {
                    leave.UpdateLeaveFromDto(model);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Only admin can change this status." });

                }
            }

            _leaveRepo.Update(leave);
            await _leaveRepo.SaveChangesAsync();

            return Ok(new { message = "Leave updated successfully" });
        }


        [HttpGet]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> LeavesList([FromQuery] SearchOptions options)
        {
            //  Check role from JWT claim
            var isAdmin = _userContext.IsAdmin(User);

            if (isAdmin)
            {
                var result = await _leaveRepo.GetAllAsync(options);
                return Ok(result);
            }
            else
            {
                var employeeId = await _userContext.GetEmployeeIdFromClaimsAsync(User);
                if (employeeId == null)
                    return Unauthorized("Employee not found.");

                var result = await _leaveRepo.GetByEmployeeIdAsync(employeeId.Value, options);
                return Ok(result);

            }

         

            
        }






    }




}
