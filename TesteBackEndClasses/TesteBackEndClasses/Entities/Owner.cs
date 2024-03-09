namespace TesteBackEndClasses.Entities
{
    public class Owner : User
    {

        public IEnumerable<Property>? Properties { get;} = new List<Property>();
    }
}
