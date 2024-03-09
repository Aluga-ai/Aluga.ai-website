
    public class Student : User
    {

        public IEnumerable<PERSONALITY> Personalitys { get; set; } = new List<PERSONALITY>();
        public IEnumerable<HOBBIE> Hobbies { get; set; } = new List<HOBBIE>();

        public IEnumerable<Student>? Connections { get;} = new List<Student>();


        public int CollegeId { get; set; }
        public College College { get; set; }

        public IEnumerable<Property>? PropertiesLikes { get; } = new List<Property>();

    }

