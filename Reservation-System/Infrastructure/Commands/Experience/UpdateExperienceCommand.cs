using MediatR;
using Reservation_System.Application.DTOs;

namespace Reservation_System.Infrastructure.Commands.Experience
{
    public record UpdateExperienceCommand(CandidateExperienceDTO candidateXP) 
        : IRequest<bool>;
    
    
}
