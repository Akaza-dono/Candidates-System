using MediatR;

namespace Reservation_System.Infrastructure.Commands.Candidates
{
    public record DeleteCandidateCommand(int idCandidate) 
        : IRequest<bool>;
    
}
