
using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using EMSBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(IEmployeeRepository employeeRepository,
             UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            
            
            )
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Get([FromQuery] SearchOptions options)
        {
            var result = await _employeeRepository.GetAllAsync(options);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee model)
        {
            // 1. Create Identity user
            var user = new User
            {
                UserName = model.Email,   // ya model.Name agar username alag lena ho
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, "Employee@123"); // fixed password

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // 2. Ensure Employee role exists
            if (!await _roleManager.RoleExistsAsync("Employee"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            // 3. Assign Employee role
            await _userManager.AddToRoleAsync(user, "Employee");

            // 4. Save employee in custom Employee table with UserId
            model.UserId = user.Id;   //  Identity user ka Id store
            await _employeeRepository.AddAsync(model);
            await _employeeRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee( int id, [FromBody] Employee model)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.LastWorkingDate = model.LastWorkingDate;
            employee.JobTitle = model.JobTitle;

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();
            return Ok();
        }

    
     [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _employeeRepository.SaveChangesAsync();
            return Ok();
        }
    } }
       