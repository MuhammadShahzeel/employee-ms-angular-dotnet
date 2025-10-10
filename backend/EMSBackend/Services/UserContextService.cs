using EMSBackend.Interfaces;
using EMSBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EMSBackend.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmployeeRepository _employeeRepo;

        public UserContextService(UserManager<User> userManager, IEmployeeRepository employeeRepo)
        {
            _userManager = userManager;
            _employeeRepo = employeeRepo;
        }

        public async Task<int?> GetEmployeeIdFromClaimsAsync(ClaimsPrincipal User)
        {
            // 1️ Extract email from JWT claims
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return null; // no email in token

            // 2️ Find the user in Identity
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null; // no user found

            // 3️ Find the employee record linked to the user
            // find employee linked to that user
            var employee = await _employeeRepo.FindAsync(e => e.UserId == user.Id);
            if (employee == null)
                return null;
        

            // 4️ Return the employee's ID
            return employee?.Id;
        }

        //  to get UserId from Claims
        public async Task<string?> GetUserIdFromClaimsAsync(ClaimsPrincipal User)
        {
            // 1️ Extract email from JWT claims
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return null;

            // 2️ Find the user in Identity
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            // 3️ Return the user's Id
            return user.Id;
        }

        public bool IsAdmin(ClaimsPrincipal User)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            return role == "Admin"; 
        }
    }


}
