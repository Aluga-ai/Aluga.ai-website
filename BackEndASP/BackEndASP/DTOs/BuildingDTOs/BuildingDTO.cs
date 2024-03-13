using System.Text.Json.Serialization;

namespace BackEndASP.DTOs.BuildingDTOs
{
    public class BuildingDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string HomeComplement { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public BuildingDTO()
        {

        }

        public BuildingDTO(Building entity)
        {

            Id = entity.Id;
            Address = entity.Address;
            Number = entity.Number;
            HomeComplement = entity.HomeComplement;
            Neighborhood = entity.Neighborhood;
            District = entity.District;
            State = entity.State;
            Lat = entity.Lat;
            Long = entity.Long;

        }


    }
}
