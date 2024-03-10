


public class Property : Building
{
        
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }

        public ICollection<Student>? StudentsLiked { get; set; } = new List<Student>();


}

