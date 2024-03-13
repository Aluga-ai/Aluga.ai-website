using BackEndASP.DTOs.BuildingDTOs;
using Correios.NET.Models;

namespace BackEndASP.Interfaces
{
    public interface IPropertyRepository
    {
        Task<BuildingResponseDTO> GetAddressByCep(string cep);

        Task<BuildingDTO> InsertBuilding(BuildingInsertDTO dto, User user);

    }
}
