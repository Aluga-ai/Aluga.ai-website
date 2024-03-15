using BackEndASP.DTOs.BuildingDTOs;

namespace BackEndASP.Interfaces
{
    public interface IBuildingRepository
    {
        Task<BuildingResponseDTO> GetAddressByCep(string cep);
    }
}
