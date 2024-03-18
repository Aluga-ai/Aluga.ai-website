using BackEndASP.DTOs.BuildingDTOs;

namespace BackEndASP.Interfaces
{
    public interface ICollegeRepository
    {
        Task InsertCollege(BuildingInsertDTO dto);
        Task AddUserToCollege(int collegeId, string userId);
    }
}
