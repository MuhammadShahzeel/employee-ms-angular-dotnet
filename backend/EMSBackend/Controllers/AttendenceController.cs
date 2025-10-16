using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using EMSBackend.Repositories;
using EMSBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendenceRepo;
        private readonly IUserContextService _userService;
        

        public AttendenceController(IAttendanceRepository attendenceRepo, IUserContextService userService)
        {
           _attendenceRepo = attendenceRepo;
            _userService = userService;
            _userService = userService;
        }

        [HttpPost("mark-present")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MarkAttendence()
        {
            var employeeId = await _userService.GetEmployeeIdFromClaimsAsync(User);

            if (employeeId == null)
                return Unauthorized("Employee not found");
            //  Check if already marked for today
            var attendenceList = await _attendenceRepo.FindAsync(
                x => x.EmployeeId == employeeId.Value &&
                     DateTime.Compare(x.Date.Date, DateTime.UtcNow.Date) == 0
            );

            if (attendenceList != null)
            {
                return BadRequest(new { message = "Already marked present for today." });
                
            }
            //If not marked, create new record
            var attendence = new Attendence
            {
                Date = DateTime.UtcNow,
                EmployeeId = employeeId.Value,
                Type = (int)AttendenceType.Present
            };

            await _attendenceRepo.AddAsync(attendence);
            await _attendenceRepo.SaveChangesAsync();

            return Ok(new { message = "Attendance marked as present successfully." });
         
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAttendanceHistory([FromQuery] SearchOptions options)
        {
            // If not admin, get employee ID from token/user context
            if (!_userService.IsAdmin(User))
            {
                options.EmployeeId = await _userService.GetEmployeeIdFromClaimsAsync(User);
            }

            var result = await _attendenceRepo.GetAttendanceHistoryAsync(options);
            return Ok(result);
        }




    }
}

