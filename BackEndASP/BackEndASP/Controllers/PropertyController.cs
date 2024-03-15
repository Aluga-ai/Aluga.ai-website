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


        [HttpPost]
        [Authorize(Policy = "OwnerOnly")]
        public async Task<ActionResult<dynamic>> InsertBuilding([FromBody] BuildingInsertDTO dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            //return CreatedAtAction(nameof(result.Id), new { id = result.Id }, result.Result);

            await _unitOfWorkRepository.PropertyRepository.InsertProperty(dto, user);
            await _unitOfWorkRepository.CommitAsync();

            return Ok("Property created successfully");
            

        }



    }
}
