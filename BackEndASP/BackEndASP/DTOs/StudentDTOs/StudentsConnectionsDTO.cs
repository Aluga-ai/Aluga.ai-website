using BackEndASP.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentsConnectionsDTO
    {
        public List<string> StudentId { get; set; } = new List<string>();

        public StudentsConnectionsDTO()
        {
            
        }

        public StudentsConnectionsDTO(List<string> connections)
        {
            foreach (string connection in connections)
            {
                this.StudentId.Add(connection);
            }
        }

        public StudentsConnectionsDTO(List<UserConnection> students)
        {
            foreach (UserConnection student in students)
            {
                this.StudentId.Add(student.OtherStudentId);
            }
        }
    }
}
