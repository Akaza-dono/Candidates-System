using MediatR;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;

namespace Reservation_System.Infrastructure.Commands.Experience
{
    public record CreateExperienceCommand(CandidateExperience candidateXP) 
        : IRequest<CandidateExperienceDTO>;
            

}
