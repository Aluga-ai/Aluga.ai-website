
    public class Property : Building
    {
        
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }

        public IEnumerable<Student>? StudentsLikes { get; } = new List<Student>();

    }

