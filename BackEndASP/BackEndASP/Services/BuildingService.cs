using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.Interfaces;

namespace BackEndASP.Services
{
    public class BuildingService : IBuildingRepository
    {
        public async Task<BuildingResponseDTO> GetAddressByCep(string cep)
        {
            try
            {
                var address = new Correios.NET.CorreiosService().GetAddresses(cep).FirstOrDefault() ?? throw new Exception("Cep does not exists");
                return new BuildingResponseDTO(address);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to call this API");
            }
        }

    }
}
