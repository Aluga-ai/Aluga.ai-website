
using BackEndASP.Entities;

public class Property : Building
{
        public double? Price {  get; set; }
        public string? Rooms { get; set; }

        public string? OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public List<PropertyStudent>? StudentProperties { get; set; } = new List<PropertyStudent>();


}

