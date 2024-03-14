using BackEndASP.DTOs.CollegeDTOs;

namespace BackEndASP.Interfaces
{
    public interface ICollegeRepository
    {
        Task InsertNewCollege(CollegeInsertDTO dto);
    }
}
