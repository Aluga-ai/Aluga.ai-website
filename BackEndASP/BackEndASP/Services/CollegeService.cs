using BackEndASP.DTOs.CollegeDTOs;
using BackEndASP.Interfaces;

namespace BackEndASP.Services
{
    public class CollegeService : ICollegeRepository
    {

        private readonly SystemDbContext _dbContext;

        public CollegeService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task InsertNewCollege(CollegeInsertDTO dto)
        {
            College entity = new College();
            copyDTOToEntity(dto, entity);
            _dbContext.Colleges.Add(entity);
            return Task.CompletedTask;
        }

        private void copyDTOToEntity(CollegeInsertDTO dto, College entity)
        {
            entity.Name = dto.Name;
            entity.State = dto.State;
            entity.Number = dto.Number;
            entity.Address = dto.Address;
            entity.Neighborhood = dto.Neighborhood;
            entity.District = dto.District;
            entity.HomeComplement = dto.HomeComplement;
        }
    }
}
