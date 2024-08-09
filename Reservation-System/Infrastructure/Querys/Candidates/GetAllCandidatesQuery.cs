using MediatR;
using Reservation_System.Application.DTOs;

namespace Reservation_System.Infrastructure.Querys.Candidates
{
    public record GetAllCandidatesQuery : IRequest<List<CandidateDTO>>;
}
