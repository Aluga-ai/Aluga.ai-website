using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEndASP.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly UserManager<User> _userManager;

        public StudentController(IUnitOfWorkRepository unitOfWorkRepository, UserManager<User> userManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _userManager = userManager;
        }



        [HttpGet("/myconnections")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<StudentsConnectionsDTO>> FindAllMyStudentsConnections()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_unitOfWorkRepository.StudentRepository.FindAllMyStudentsConnections(userId));
        }

        [HttpGet("/myinvitationsforconnection")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<StudentsConnectionsDTO>> FindMyAllStudentsWhoInvitationsConnections()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_unitOfWorkRepository.StudentRepository.FindMyAllStudentsWhoInvitationsConnections(userId));
        }


        // adicionando o ID de um estudante como pedido de conexão
        [HttpPut("{studentForConnectionId}")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> GiveConnectionOrder(string studentForConnectionId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _unitOfWorkRepository.StudentRepository.GiveConnectionOrder(userId, studentForConnectionId);
                await _unitOfWorkRepository.CommitAsync();
                return Ok("Order sent successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(400, "An error occurred while processing the request.");
            }
        }



        // estudante aceitando ou recusando o pedido de conexão
        [HttpPost]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult<dynamic>> GiveConnectionOrder([FromBody] StudentConnectionInsertDTO dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _unitOfWorkRepository.StudentRepository.HandleConnection(userId, dto);
                await _unitOfWorkRepository.CommitAsync();
                if (!result)
                {
                    return BadRequest("Connection refused");
                }
                return Ok("Connection accepted");
                
            } catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
