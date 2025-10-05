using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using EMSBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EMSBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
       
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDepartment([FromBody] Department model)
        {
            await _departmentRepository.AddAsync(model);
            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateDepartment(int id, [FromBody] Department model)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            department.Name = model.Name;
            _departmentRepository.Update(department);

            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getAllDepartment([FromQuery] SearchOptions options)
        {
            var result = await _departmentRepository.GetAllAsync(options);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteAsync(id);


            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

    }
}
