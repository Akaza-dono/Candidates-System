using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Infrastructure.Commands.Experience;
using Reservation_System.Infrastructure.Context;

namespace Reservation_System.Application.Handlers.Experience
{
    public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, bool>
    {
        private readonly ReservationDbContext _reservationDbContext;

        public UpdateExperienceHandler(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public async Task<bool> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidateExperience = await _reservationDbContext.CandidateExperiences
                .FirstOrDefaultAsync(c => c.IdCandidateExperience == request.candidateXP.IdCandidateExperience, cancellationToken);

            if (candidateExperience != null)
            {
                candidateExperience.Company = request.candidateXP.Company;
                candidateExperience.Job = request.candidateXP.Job;
                candidateExperience.Description = request.candidateXP.Description;
                candidateExperience.Salary = request.candidateXP.Salary;
                candidateExperience.BeginDate = request.candidateXP.BeginDate;
                candidateExperience.EndDate = request.candidateXP.EndDate;
                candidateExperience.InsertDate = request.candidateXP.InsertDate;
                candidateExperience.ModifyDate = DateTime.Now;

                int rowsAffected = await _reservationDbContext.SaveChangesAsync(cancellationToken);

                return rowsAffected > 0;
            }
            return false;
        }
    }
}
