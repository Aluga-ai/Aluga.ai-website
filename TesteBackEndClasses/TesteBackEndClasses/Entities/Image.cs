namespace TesteBackEndClasses.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public Byte[] ImageData { get; set; }


        public int BuildingId { get; set; }
        public IEnumerable<Building> Buildings{ get; } = new List<Building>();

        public IEnumerable<Student> Students { get; } = new List<Student>();
    }
}
