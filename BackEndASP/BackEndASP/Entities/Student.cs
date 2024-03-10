public class Student : User
{
    public List<PERSONALITY>? Personalitys { get; set; } = new List<PERSONALITY>();
    public List<HOBBIE>? Hobbies { get; set; } = new List<HOBBIE>();
    public IEnumerable<Student>? Connections { get; set; } = new List<Student>();
    public int CollegeId { get; set; }
    public College College { get; set; }
    public IEnumerable<Property>? PropertiesLiked { get; set; } = new List<Property>();
}
