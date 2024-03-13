
using BackEndASP.Entities;

public class Property : Building
{
        
        public string? OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public List<PropertyStudent>? StudentProperties { get; set; } = new List<PropertyStudent>();


}

