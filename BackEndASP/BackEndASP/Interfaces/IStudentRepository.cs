using BackEndASP.DTOs.StudentDTOs;

namespace BackEndASP.Interfaces
{
    public interface IStudentRepository
    {

        StudentsConnectionsDTO FindAllMyStudentsConnections(string userId);
        StudentsConnectionsDTO FindMyAllStudentsWhoInvitationsConnections(string userId);
        Task GiveConnectionOrder(string userId, string studentForConnectionId);
        Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto, int notificationId);

    }
}
