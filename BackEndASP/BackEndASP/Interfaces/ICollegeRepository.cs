using BackEndASP.DTOs.BuildingDTOs;

namespace BackEndASP.Interfaces
{
    public interface ICollegeRepository
    {
        Task InsertCollege(BuildingInsertDTO dto);
    }
}
