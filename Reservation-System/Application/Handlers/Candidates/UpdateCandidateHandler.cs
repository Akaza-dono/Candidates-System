using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Candidates
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, bool>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public UpdateCandidateHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public Task<bool> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = _reservationDbContext.Candidate
                .FirstOrDefault(c => c.IdCandidate == request.Candidate.IdCandidate);

            if (candidate == null)
            {
                return Task.FromResult(false);
            }

            candidate.Name = request.Candidate.Name;
            candidate.Surname = request.Candidate.Surname;
            candidate.Birthdate = request.Candidate.Birthdate;
            candidate.Email = request.Candidate.Email;
            candidate.InsertDate = request.Candidate.InsertDate;
            candidate.ModifyDate = DateTime.Now;

            _reservationDbContext.SaveChanges();

            return Task.FromResult(true);

        }
    }
}