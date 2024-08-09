using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Commands.Experience;
using Reservation_System.Infrastructure.Querys.Candidates;
using Reservation_System.Infrastructure.Querys.Experience;
using Reservation_System.Models;

namespace Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExperienceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetExperience/{idCandidate}")]
        public async Task<ActionResult<List<CandidateExperienceDTO>>> GetExperience(int idCandidate)
        {
            try
            {
                List<CandidateExperienceDTO> candidatesXP = await _mediator.Send(new GetExperienceByIdQuery(idCandidate));
                if (candidatesXP.Count == 0)
                {
                    return NotFound(new { Error = true, Message = "No hay experiencias con este candidato" });
                }
                return Ok(candidatesXP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "No se pudieron obtener las experiencias", Details = ex.Message });
            }
        }

        [HttpPost("CreateExperience")]
        public async Task<ActionResult<CandidateExperienceDTO>> CreateExperience(CandidateExperience idCandidate)
        {
            try
            {
                CandidateExperienceDTO candidatesXP = await _mediator.Send(new CreateExperienceCommand(idCandidate));
                return Ok(candidatesXP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "No se pudieron obtener las experiencias", Details = ex.Message });
            }
        }

        [HttpDelete("DeleteExperience/{IdExperience}")]
        public async Task<ActionResult<bool>> DeleteCandidate(int IdExperience)
        {
            try
            {
                bool result = await _mediator.Send(new DeleteExperienceCommand(IdExperience));
                if (!result)
                {
                    return NotFound(new { MessageError = $"No fue encontrada la experiencia con id {IdExperience}" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "Error al eliminar el candidato.", Details = ex.Message });

            }
        }

        [HttpPut("UpdateExperience")]
        public async Task<ActionResult<bool>> UpdateCandidate(CandidateExperienceDTO candidate)
        {
            try
            {
                bool result = await _mediator.Send(new UpdateExperienceCommand(candidate));
                if (!result)
                {
                    return NotFound(new { MessageError = $"El candidato con id {candidate.IdCandidate} no fue encontrado para actualizar." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "Error al actualizar candidato", Details = ex.Message });
            }
        }
    }
}
