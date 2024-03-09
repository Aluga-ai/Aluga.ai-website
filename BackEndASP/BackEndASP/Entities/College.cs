
public class College : Building
{

    public IEnumerable<Student> Students { get; set; } = new List<Student>();

}