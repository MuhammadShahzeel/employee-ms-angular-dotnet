
using EMSBackend.Interfaces;
using EMSBackend.Models;

using Microsoft.AspNetCore.Mvc;

namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee model)
        {
            await _employeeRepository.AddAsync(model);
            await _employeeRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            await _employeeRepository.SaveChangesAsync();
            return Ok();
        }
    } }
       