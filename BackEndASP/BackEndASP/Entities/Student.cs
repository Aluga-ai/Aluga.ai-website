using BackEndASP.Entities;

public class Student : User
{
    public List<PERSONALITY>? Personalitys { get; set; } = new List<PERSONALITY>();
    public List<HOBBIE>? Hobbies { get; set; } = new List<HOBBIE>();
    public List<UserConnection>? Connections { get; set; } = new List<UserConnection>();
    public int CollegeId { get; set; }
    public College College { get; set; }
    public List<PropertyStudent>? StudentProperties { get; set; } = new List<PropertyStudent>();

}
