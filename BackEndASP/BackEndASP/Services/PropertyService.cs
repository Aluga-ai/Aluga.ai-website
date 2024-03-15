using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.ExternalAPI;
using BackEndASP.ExternalAPI.GeoCoder;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Correios.NET.Models;
using Geocoding;
using Geocoding.Google;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;


namespace BackEndASP.Services
{
    public class PropertyService : IPropertyRepository
    {

        private readonly SystemDbContext _dbContext;

        public PropertyService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task InsertProperty(BuildingInsertDTO dto, User user)
        {
             Property entity = new Property();
             copyDTOToEntity(dto, entity, user);

                
             var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
               $"?address={ConvertAddress.Convert(dto)}" +
               $"&inputtype=textquery&fields=geometry" +
               $"&key={APIKey.key}";

             var json = new WebClient().DownloadString(targetUrl);
          

            GoogleGeoCodeResponse response = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json);

            entity.Lat = response.results[0].geometry.location.lat;
            entity.Long = response.results[0].geometry.location.lng;

            _dbContext.Properties.Add(entity);
            return Task.CompletedTask;

        }

        private void copyDTOToEntity(BuildingInsertDTO dto, Property entity, User user)
        {
            entity.Address = dto.Address;
            entity.Neighborhood = dto.Neighborhood;
            entity.District = dto.District;
            entity.State = dto.State;
            entity.Number = dto.Number;
            entity.HomeComplement = dto.HomeComplement;
            entity.OwnerId = user.Id;
            entity.Rooms = dto.Rooms;
            entity.Price = dto.Price;
        }

        


        
    }
}
