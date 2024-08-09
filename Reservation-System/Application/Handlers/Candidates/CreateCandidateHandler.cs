using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Candidates
{
    public class CreateCandidateHandler
        : IRequestHandler<CreateCandidateCommand, CandidateDTO>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public CreateCandidateHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }

        public async Task<CandidateDTO> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool mailExist = await _reservationDbContext.Candidate
                    .AnyAsync(c => c.Email == request.Candidate.Email);

                if (mailExist)
                {
                    throw new InvalidOperationException("El correo electrónico ya existe.");
                }
                Candidate candidate = new()
                {
                    Name = request.Candidate.Name,
                    Surname = request.Candidate.Surname,
                    Birthdate = request.Candidate.Birthdate,
                    Email = request.Candidate.Email,
                    InsertDate = DateTime.Now,
                    ModifyDate = new DateTime(1000, 1, 1),
                };

                _reservationDbContext.Candidate.Add(candidate);
                await _reservationDbContext.SaveChangesAsync();

                return new CandidateDTO
                {
                    IdCandidate = candidate.IdCandidate,
                    Name = candidate.Name,
                    Surname = candidate.Surname,
                    Birthdate = candidate.Birthdate,
                    Email = candidate.Email,
                    InsertDate = DateTime.Now,
                };
            }
            catch (DbUpdateException ex)
            {

                throw ex;
            }
        }
    }
}
