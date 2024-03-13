
public class College : Building
{
    public string? Name { get; set; }
    public ICollection<Student>? Students { get; set; } = new List<Student>();

}