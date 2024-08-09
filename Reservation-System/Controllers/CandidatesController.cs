using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Querys.Candidates;
using Reservation_System.Models;

namespace Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCandidates")]
        public async Task<ActionResult<List<CandidateDTO>>> GetAllCandidates()
        {
            try
            {
                List<CandidateDTO> candidates = await _mediator.Send(new GetAllCandidatesQuery());
                if (candidates.Count > 0)
                {
                    return Ok(candidates);
                }
                return NotFound(new { Error = true, Message = "No hay un lista de candidatos" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "No se pudieron obtener todos los candidatos", Details = ex.Message });
            }
        }

        [HttpGet("GetCandidateById/{IdCandidate}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateById(int IdCandidate)
        {
            try
            {
                CandidateDTO candidate = await _mediator.Send(new GetCandidateByIdQuery(IdCandidate));
                if (candidate == null)
                {
                    return NotFound(new { MessageError = $"El candidto con id {IdCandidate} no fue encontrado" });
                }
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "Error al procesar la solicitud,", Details = ex.Message });

            }
        }

        [HttpPost("CreateCandidate")]
        public async Task<ActionResult<CandidateDTO>> CreateCandidate(Candidate candidate)
        {
            try
            {
                CandidateDTO createdCandidate = await _mediator.Send(new CreateCandidateCommand(candidate));
                return CreatedAtAction(nameof(GetCandidateById), new { IdCandidate = createdCandidate }, createdCandidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "Error al procesar la solicitud,", Details = ex.Message });
            }
        }

        [HttpPut("UpdateCandidate")]
        public async Task<ActionResult<bool>> UpdateCandidate(CandidateDTO candidate)
        {
            try
            {
                bool result = await _mediator.Send(new UpdateCandidateCommand(candidate));
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

        [HttpDelete("DeleteCandidate/{IdCandidate}")]
        public async Task<ActionResult<bool>> DeleteCandidate(int IdCandidate)
        {
            try
            {
                bool result = await _mediator.Send(new DeleteCandidateCommand(IdCandidate));
                if (!result)
                {
                    return NotFound(new { MessageError = $"El candidato con id {IdCandidate} no fue encontrado para eliminar." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel { Message = "Error al eliminar el candidato.", Details = ex.Message });

            }
        }
    }
}
