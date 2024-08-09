using MediatR;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Context;
using Reservation_System.Infrastructure.Querys.Candidates;

namespace Reservation_System.Application.Handlers.Candidates
{
    public class GetAllCandidatesHandler : IRequestHandler<GetAllCandidatesQuery, List<CandidateDTO>>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public GetAllCandidatesHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public async Task<List<CandidateDTO>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            List<CandidateDTO> candidateDTOs = await _reservationDbContext.Candidate.Select(c => new CandidateDTO
            {
                IdCandidate = c.IdCandidate,
                Name = c.Name,
                Surname = c.Surname,
                Birthdate = c.Birthdate,
                Email = c.Email,
                InsertDate = c.InsertDate,
                ModifyDate = c.ModifyDate
            }).ToListAsync(cancellationToken);

            return candidateDTOs;
        }
    }
}
