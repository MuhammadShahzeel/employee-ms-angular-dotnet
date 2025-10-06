using EMSBackend.Dtos;
using EMSBackend.Models;

namespace EMSBackend.Mappers
{
    public static class LeaveMappers
    {
        public static Leave ToLeave(this LeaveDto dto)
        {
            return new Leave
            {
                Type = (int)dto.Type,
                Reason = dto.Reason,
                LeaveDate = dto.LeaveDate,
                Status = (int)LeaveStatus.Pending,
                EmployeeId = (int)dto.EmployeeId
            };
        }

    }
}
