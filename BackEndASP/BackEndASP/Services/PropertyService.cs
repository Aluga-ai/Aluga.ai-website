using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.ExternalAPI;
using BackEndASP.ExternalAPI.GeoCoder;
using BackEndASP.Interfaces;
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
        private readonly string _key = "AIzaSyBl9tDHzTnO6-Nw8tmUNc_gjv5xAoh6Saw";

        public PropertyService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuildingResponseDTO> GetAddressByCep(string cep)
        {
            try
            {
                var address = new Correios.NET.CorreiosService().GetAddresses(cep).FirstOrDefault() ?? throw new Exception("Cep does not exists");
                return new BuildingResponseDTO(address);

            } catch (Exception ex)
            {
                throw new Exception("Failed to call this API");
            }
        }

        public async Task<BuildingDTO> InsertBuilding(BuildingInsertDTO dto, User user)
        {
             Property entity = new Property();
             copyDTOToEntity(dto, entity, user);

                
             var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
               $"?address={ConvertAddress(dto)}" +
               $"&inputtype=textquery&fields=geometry" +
               $"&key={_key}";

             var json = new WebClient().DownloadString(targetUrl);
          

            GoogleGeoCodeResponse response = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json);

            entity.Lat = response.results[0].geometry.location.lat;
            entity.Long = response.results[0].geometry.location.lng;

            _dbContext.Properties.Add(entity);
            return new BuildingDTO(entity);

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
        }

        private string ConvertAddress(BuildingInsertDTO dto)
        {
            var data = $"{dto.Number} {dto.Address} {dto.District} {dto.State} Brasil";
            return Uri.EscapeDataString(data);
        }


        
    }
}
