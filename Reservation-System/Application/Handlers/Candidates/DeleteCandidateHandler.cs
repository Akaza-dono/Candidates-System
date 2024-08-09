using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Candidates
{
    public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public DeleteCandidateHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }

        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _reservationDbContext.Candidate.Where(c => c.IdCandidate == request.idCandidate).FirstAsync();
            if (candidate == null)
            {
                return false;
            }
            _reservationDbContext.Candidate.Remove(candidate);
            _reservationDbContext.SaveChanges();
            return true;
        }
    }
}
