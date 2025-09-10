using EMSBackend.Interfaces;
using EMSBackend.Models;
using Microsoft.AspNetCore.Mvc;
namespace EMSBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentController(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department model)
        {
            await _departmentRepository.AddAsync(model);
            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDepartment(int id, [FromBody] Department model)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            department.Name = model.Name;
            _departmentRepository.Update(department);

            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> getAllDepartment()
        {
            var list = await _departmentRepository.GetAllAsync();

            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteAsync(id);


            await _departmentRepository.SaveChangesAsync();
            return Ok();
        }

    }
}
