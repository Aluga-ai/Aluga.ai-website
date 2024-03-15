using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.Interfaces;
using Correios.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public BuildingController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<BuildingResponseDTO>> GetAddressByCep(string cep)
        {
            try
            {
                return Ok(_unitOfWorkRepository.BuildingRepository.GetAddressByCep(cep));
            } catch(Exception ex) 
            {
                return BadRequest(ex.Message);}
            }



      
    }
}
