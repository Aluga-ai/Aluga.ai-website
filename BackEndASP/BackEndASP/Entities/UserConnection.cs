namespace BackEndASP.Entities
{
    public class UserConnection
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public string OtherStudentId { get; set; }
        public Student OtherStudent { get; set; }
    }
}
