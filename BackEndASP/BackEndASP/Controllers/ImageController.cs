using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Interfaces;
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

        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public ImageController(UserManager<User> userManager, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _userManager = userManager;
            _unitOfWorkRepository = unitOfWorkRepository;
        }


        [HttpPost("user")]
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

    }
}
