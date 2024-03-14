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
    [Route("properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly UserManager<User> _userManager;

        public PropertyController(IUnitOfWorkRepository unitOfWorkRepository, UserManager<User> userManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _userManager = userManager;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<BuildingResponseDTO>> GetAddressByCep(string cep)
        {
            try
            {
                return Ok(_unitOfWorkRepository.PropertyRepository.GetAddressByCep(cep));
            } catch(Exception ex) 
            {
                return BadRequest(ex.Message);}
            }



        [HttpPost]
        [Authorize(Policy = "OwnerOnly")]
        public async Task<ActionResult<BuildingDTO>> InsertBuilding([FromBody] BuildingInsertDTO dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var isInOwnerRole = await _userManager.IsInRoleAsync(user, "Owner");
            //return CreatedAtAction(nameof(result.Id), new { id = result.Id }, result.Result);

            if (isInOwnerRole)
           {
                Task<BuildingDTO> result = _unitOfWorkRepository.PropertyRepository.InsertBuilding(dto, user);
                await _unitOfWorkRepository.CommitAsync();

                return Ok(result);
            } else
            {
                return BadRequest("This user isnt OWNER");
            }

        }



    }
}
