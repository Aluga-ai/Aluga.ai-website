using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using Geocoding;
using Microsoft.EntityFrameworkCore;

namespace BackEndASP.Services
{
    public class StudentService : IStudentRepository
    {

        private readonly SystemDbContext _dbContext;

        public StudentService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // busca todas minhas conexões
        public StudentsConnectionsDTO FindAllMyStudentsConnections(string userId)
        {
            Student student = _dbContext.Students
                .Include(s => s.Connections)
                .AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            return new StudentsConnectionsDTO(student.Connections);
        }

        //busca todas as solicitações que eu tenho pendente
        public StudentsConnectionsDTO FindMyAllStudentsWhoInvitationsConnections(string userId)
        {
            Student student = _dbContext.Students.AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");
            
            return new StudentsConnectionsDTO(student.PendentsConnectionsId);
        }


        // adicionando o ID de um estudante como pedido de conexão
        public async Task GiveConnectionOrder(string userId, string studentForConnectionId)
        {
            Student student = await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentForConnectionId) ??
                throw new ArgumentException($"This id {userId} does not exists");

            student.PendentsConnectionsId.Add(userId);
            _dbContext.Update(student);
        }


        // estudante aceitando ou recusando o pedido de conexão
        public async Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto)
        {
            Student student = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == userId) ?? throw new ArgumentException($"This id {userId} does not exist");

            if (student.PendentsConnectionsId != null)
            {
                student.PendentsConnectionsId.Remove(dto.ConnectionWhyIHandle);
                _dbContext.Students.Update(student);

                if (dto.Action)
                {
                    UserConnection userConnection = new UserConnection
                    {
                        StudentId = userId,
                        OtherStudentId = dto.ConnectionWhyIHandle
                    };
                    await _dbContext.UserConnections.AddAsync(userConnection);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
