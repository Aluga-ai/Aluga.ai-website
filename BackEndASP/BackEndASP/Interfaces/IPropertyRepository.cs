using BackEndASP.DTOs.BuildingDTOs;
using Correios.NET.Models;

namespace BackEndASP.Interfaces
{
    public interface IPropertyRepository
    {
        Task InsertProperty(BuildingInsertDTO dto, User user);

    }
}
