namespace BackEndASP.Entities
{
    public class PropertyStudent
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

    }
}
