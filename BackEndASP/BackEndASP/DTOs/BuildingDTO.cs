using System.Text.Json.Serialization;

namespace BackEndASP.DTOs
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
            
            this.Id = entity.Id;
            this.Address = entity.Address;
            this.Number = entity.Number;
            this.HomeComplement = entity.HomeComplement;
            this.Neighborhood = entity.Neighborhood;
            this.District = entity.District;
            this.State = entity.State;
            this.Lat = entity.Lat;
            this.Long = entity.Long;

        }

      
    }
}
