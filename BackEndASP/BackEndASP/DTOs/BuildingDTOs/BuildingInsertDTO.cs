namespace BackEndASP.DTOs.BuildingDTOs
{
    public class BuildingInsertDTO
    {
  
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string? Number { get; set; }
        public string? HomeComplement { get; set; }
        public string? Name { get; set; }
        public string? Rooms { get; set; }
        public double? Price { get; set; }

    }
}
