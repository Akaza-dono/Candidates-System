using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Infrastructure.Commands.Experience;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Experience
{
    public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, bool>
    {

        private readonly ReservationDbContext _reservationDbContext;

        public DeleteExperienceHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidateExperience = await _reservationDbContext.CandidateExperiences
                .FirstOrDefaultAsync(c => c.IdCandidateExperience == request.idExperience, cancellationToken);

            if (candidateExperience == null)
            {
                return false;
            }

            _reservationDbContext.CandidateExperiences.Remove(candidateExperience);
            _reservationDbContext.SaveChanges();

            return true;
        }
    }
}
