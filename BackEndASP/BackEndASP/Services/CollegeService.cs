using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.ExternalAPI.GeoCoder;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace BackEndASP.Services
{
    public class CollegeService : ICollegeRepository
    {

        private readonly SystemDbContext _dbContext;

        public CollegeService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public Task InsertCollege(BuildingInsertDTO dto)
        {
            College entity = new College();
            copyDTOToEntity(dto, entity);


            var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
              $"?address={ConvertAddress.Convert(dto)}" +
              $"&inputtype=textquery&fields=geometry" +
              $"&key={APIKey.key}";

            var json = new WebClient().DownloadString(targetUrl);


            GoogleGeoCodeResponse response = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json);

            entity.Lat = response.results[0].geometry.location.lat;
            entity.Long = response.results[0].geometry.location.lng;

            _dbContext.Colleges.Add(entity);
            return Task.CompletedTask;
        }


        public Task AddUserToCollege(int collegeId, string userId)
        {
            Student student = _dbContext.Students
                .Include(s => s.College)
                .AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            College college = _dbContext.Colleges.AsNoTracking().FirstOrDefault(c => c.Id == collegeId) 
                ?? throw new ArgumentException($"This id {collegeId} does not exist");

            student.College = college;
            _dbContext.Students.Update(student);
            return Task.CompletedTask;
        }




        private void copyDTOToEntity(BuildingInsertDTO dto, College entity)
        {
            entity.Name = dto.Name;
            entity.State = dto.State;
            entity.Address = dto.Address;
            entity.Neighborhood = dto.Neighborhood;
            entity.District = dto.District;
        }
    }
}
