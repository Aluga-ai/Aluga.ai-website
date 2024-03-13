using BackEndASP.ExternalAPI;
using Correios.NET.Models;

namespace BackEndASP.DTOs.BuildingDTOs
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

            Address = entity.Address;
            Neighborhood = entity.Neighborhood;
            District = entity.District;
            State = entity.State;

        }


        public BuildingResponseDTO(Address entity)
        {
            Address = entity.Street;
            Neighborhood = entity.District;
            District = entity.City;
            State = entity.State;
        }

    }
}
