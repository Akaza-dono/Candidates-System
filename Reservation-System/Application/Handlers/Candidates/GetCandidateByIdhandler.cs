using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Infrastructure.Context;
using Reservation_System.Infrastructure.Querys.Candidates;

namespace Reservation_System.Application.Handlers.Candidates
{
    public class GetCandidateByIdhandler : IRequestHandler<GetCandidateByIdQuery, CandidateDTO>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public GetCandidateByIdhandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public async Task<CandidateDTO> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            CandidateDTO candidate = await _reservationDbContext.Candidate
                .Where(c => c.IdCandidate == request.idCandidate)
                .Select(c => new CandidateDTO
            {
                IdCandidate = request.idCandidate,
                Name = c.Name,
                Surname = c.Surname,
                Birthdate = c.Birthdate,
                Email = c.Email,
                InsertDate = c.InsertDate,
                ModifyDate = c.ModifyDate
            }).FirstAsync(cancellationToken);

            return candidate;
        }
    }
}
