using MediatR;
using Reservation_System.Application.DTOs;

namespace Reservation_System.Infrastructure.Querys.Experience
{
    public record GetExperienceByIdQuery(int idCandidate) : IRequest<List<CandidateExperienceDTO>>;
    
}
