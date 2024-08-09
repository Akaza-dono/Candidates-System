using MediatR;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Commands.Experience;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Experience
{
    public class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, CandidateExperienceDTO>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public CreateExperienceHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }

        public async Task<CandidateExperienceDTO> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {

            CandidateExperience candidateXP = new()
            {
                IdCandidate = request.candidateXP.IdCandidate,
                Company = request.candidateXP.Company,
                Job = request.candidateXP.Job,
                Description = request.candidateXP.Description,
                Salary = request.candidateXP.Salary,
                BeginDate = request.candidateXP.BeginDate,
                EndDate = request.candidateXP.EndDate,
                InsertDate = DateTime.Now
            };

            _reservationDbContext.CandidateExperiences.Add(candidateXP);
            await _reservationDbContext.SaveChangesAsync();

            return new CandidateExperienceDTO
            {
                IdCandidateExperience = candidateXP.IdCandidateExperience,
                Company = candidateXP.Company,
                Job = candidateXP.Job,
                Description = candidateXP.Description,
                Salary = candidateXP.Salary,
                BeginDate = candidateXP.BeginDate,
                EndDate = candidateXP.EndDate,
                InsertDate = candidateXP.InsertDate
            };
        }
    }
}
