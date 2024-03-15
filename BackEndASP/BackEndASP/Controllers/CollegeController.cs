
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
