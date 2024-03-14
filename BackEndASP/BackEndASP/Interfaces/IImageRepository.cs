using BackEndASP.DTOs.ImageDTOs;

namespace BackEndASP.Interfaces
{
    public interface IImageRepository
    {
        Task InsertImageForAUser(ImageUserInsertDTO dto, string userId);
        Task InsertImageForBuilding(ImageBuildingInsertDTO dto, string userId, int propertyId);
    }
}
