using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("images")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public ImageController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }


        [HttpPost("user")]
        [Authorize(Policy = "StudentOrOwner")]
        public async Task<ActionResult<dynamic>> InsertImageForAUser([FromForm] ImageUserInsertDTO dto)
        {

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.ImageRepository.InsertImageForAUser(dto, userId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Image saved successfuly");

            } catch (ArgumentException ex) 
            { 
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("building/{propertyId}")]
        [Authorize(Policy = "Owner")]
        public async Task<ActionResult<dynamic>> InsertImageForBuilding([FromForm] ImageBuildingInsertDTO dto, int propertyId)
        {
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.ImageRepository.InsertImageForBuilding(dto, userId, propertyId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Image saved successfuly");

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
