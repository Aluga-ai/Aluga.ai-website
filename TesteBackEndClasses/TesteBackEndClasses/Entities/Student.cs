using TesteBackEndClasses.Entities.enums;

namespace TesteBackEndClasses.Entities
{
    public class Student : User
    {

        public PERSONALITY Personality { get; set; }
        public HOBBIE Hobbie { get; set; }

        public IEnumerable<Student>? Connection { get;} = new List<Student>();


        public int CollegeId { get; set; }
        public College College { get; set; }

        public IEnumerable<Property>? PropertyLikes { get; } = new List<Property>();

    }
}
