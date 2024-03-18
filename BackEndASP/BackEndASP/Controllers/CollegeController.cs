
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("colleges")]
    [ApiController]
    public class CollegeController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public CollegeController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }




        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<dynamic>> InsertCollege([FromBody] BuildingInsertDTO dto)
        {
            await _unitOfWorkRepository.CollegeRepository.InsertCollege(dto);
            await _unitOfWorkRepository.CommitAsync();
            return Ok("College created successfully");
        }


        [HttpPut("{collegeId}")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> AddUserToCollege(int collegeId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.CollegeRepository.AddUserToCollege(collegeId, userId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Student add to this College successfully");
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }


    }
}
