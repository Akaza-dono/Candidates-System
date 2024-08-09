using MediatR;

namespace Reservation_System.Infrastructure.Commands.Experience
{
    public record DeleteExperienceCommand(int idExperience) 
        : IRequest<bool>;
    
}
