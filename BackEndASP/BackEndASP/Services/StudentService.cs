using ApiCatalogo.Pagination;
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


        // busca todos o usuários com filtros de paginação, idade, faculdade, etc
        public async Task<IEnumerable<StudentGetAllFilterDTO>> FindAllStudentsAsync(PageQueryParams pageQueryParams, string userId)
        {
            DateTime initialAge = DateTime.Today.AddYears(-pageQueryParams.InitialAge);
            DateTime finalAge = DateTime.Today.AddYears(-pageQueryParams.FinalAge);


            IQueryable<Student> query = _dbContext.Students.Include(s => s.College).AsNoTracking();


            if (pageQueryParams.OwnCollege)
            {
                Student student = await query.FirstOrDefaultAsync(s => s.Id == userId) ?? throw new ArgumentException($"This id {userId} does not exist");

                int? collegeId = student.CollegeId;

                query = query.Where(s => s.CollegeId == collegeId);
            }

            if (!string.IsNullOrEmpty(pageQueryParams.Name))
            {
                query = query.Where(s => s.UserName.ToUpper().Contains(pageQueryParams.Name.ToUpper()));
            }

            query = query.Where(s => s.BirthDate <= initialAge && s.BirthDate >= finalAge);

            query = query.Skip((pageQueryParams.PageNumber - 1) * pageQueryParams.PageSize).Take(pageQueryParams.PageSize);

            query = query.Where(s => s.Id != userId);

            List<StudentGetAllFilterDTO> result = await query.Select(s => new StudentGetAllFilterDTO(s)).ToListAsync();

            return result;
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
            Student studentForConnection = await _dbContext.Students.FindAsync(studentForConnectionId)
                ?? throw new ArgumentException($"User with id {studentForConnectionId} does not exist");

            var user = await _dbContext.Users.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            var userNotification = new UserNotifications
            {
                User = user,
                Notification = new Notification
                {
                    Moment = DateTimeOffset.Now,
                    Read = false,
                    Text = $"O usuário {user.UserName.ToUpper()} enviou a você um pedido de conexão!"
                }
            };

            // Add the UserNotification to the appropriate collections
            studentForConnection.UserNotifications.Add(userNotification);
            studentForConnection.PendentsConnectionsId.Add(userId);
            _dbContext.Update(studentForConnection);
        }


        // estudante aceitando ou recusando o pedido de conexão
        public async Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto, int notificationId)
        {
            // Encontrar o estudante atual
            Student actualStudent = await _dbContext.Students
                .Include(s => s.UserNotifications)
                .FirstOrDefaultAsync(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            // Encontrar a notificação
            Notification notification = await _dbContext.Notifications
                .FirstOrDefaultAsync(s => s.Id == notificationId)
                ?? throw new ArgumentException($"This id {notificationId} does not exist");

            // Marcar a notificação como lida
            notification.Read = true;

            // Remover a conexão de PendentsConnectionsId
            if (actualStudent.PendentsConnectionsId != null && actualStudent.PendentsConnectionsId.Contains(dto.ConnectionWhyIHandle))
            {
                actualStudent.PendentsConnectionsId.Remove(dto.ConnectionWhyIHandle);

                if (dto.Action)
                {
                    // Se a ação for verdadeira, a conexão é aceita
                    UserConnection userConnection = new UserConnection
                    {
                        StudentId = userId,
                        OtherStudentId = dto.ConnectionWhyIHandle
                    };

                    // Criar a notificação
                    Notification sendNotificationForUser = new Notification
                    {
                        Moment = DateTimeOffset.Now,
                        Read = false,
                        Text = $"O usuário {actualStudent.UserName.ToUpper()} aceitou o seu pedido de conexão!"
                    };

                    // Adicionar a notificação ao contexto do banco de dados
                    _dbContext.Notifications.Add(sendNotificationForUser);

                    // Salvar as alterações para obter o ID da notificação atribuído automaticamente
                    await _dbContext.SaveChangesAsync();

                    // Criar o relacionamento entre o usuário e a notificação
                    var userNotification = new UserNotifications
                    {
                        UserId = dto.ConnectionWhyIHandle,
                        NotificationId = sendNotificationForUser.Id // Usar o ID atribuído da nova notificação
                    };

                    // Adicionar a notificação ao conjunto de notificações do outro estudante
                    Student otherStudent = await _dbContext.Students
                        .FirstOrDefaultAsync(s => s.Id == dto.ConnectionWhyIHandle)
                        ?? throw new ArgumentException($"This id {userId} does not exist");

                    otherStudent.UserNotifications.Add(userNotification);

                    // Adicionar a nova conexão ao contexto do banco de dados
                    await _dbContext.UserConnections.AddAsync(userConnection);
                    _dbContext.Students.Update(otherStudent);
                }

                // Atualizar o estudante atual no contexto do banco de dados
                _dbContext.Students.Update(actualStudent);

                // Salvar as alterações no banco de dados
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }




    }
}
