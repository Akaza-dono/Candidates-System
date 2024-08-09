using MediatR;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;

namespace Reservation_System.Infrastructure.Commands.Candidates
{
    public record CreateCandidateCommand(Candidate Candidate) 
        : IRequest<CandidateDTO>;
   
    
}
