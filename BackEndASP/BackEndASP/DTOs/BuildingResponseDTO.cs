using BackEndASP.ExternalAPI;
using Correios.NET.Models;

namespace BackEndASP.DTOs
{
    public class BuildingResponseDTO
    {

        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }


        public BuildingResponseDTO()
        {
            
        }

        public BuildingResponseDTO(Building entity)
        {
            
            this.Address = entity.Address;
            this.Neighborhood = entity.Neighborhood;
            this.District = entity.District;
            this.State = entity.State;

        }


        public BuildingResponseDTO(Address entity)
        {
            this.Address = entity.Street;
            this.Neighborhood = entity.District;
            this.District = entity.City;
            this.State = entity.State;
        }

    }
}
