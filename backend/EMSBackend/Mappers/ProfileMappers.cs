using EMSBackend.Dtos;
using EMSBackend.Models;

namespace EMSBackend.Mappers
{
    public static class ProfileMappers
    {
        public static void UpdateUserFromDto(this User user, ProfileDto dto)
        {
            user.Email = dto.Email;
            user.UserName = dto.Email; // Identity ka required field
            user.ProfileImage = dto.ProfileImage;
            // Password yahan nahi update karenge, woh UserManager karega
        }

        public static void UpdateEmployeeFromDto(this Employee employee, ProfileDto dto)
        {
            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
        }
    }
}
