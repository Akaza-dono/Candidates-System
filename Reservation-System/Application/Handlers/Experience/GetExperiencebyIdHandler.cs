using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Application.DTOs;
using Reservation_System.Infrastructure.Context;
using Reservation_System.Infrastructure.Querys.Experience;

namespace Reservation_System.Application.Handlers.Experience;
public class GetExperiencebyIdHandler : IRequestHandler<GetExperienceByIdQuery, List<CandidateExperienceDTO>>
{
    private readonly ReservationDbContext _reservationDbContext;

    public GetExperiencebyIdHandler(ReservationDbContext reservationDbContext)
    {
        _reservationDbContext = reservationDbContext;
    } 
    public async Task<List<CandidateExperienceDTO>> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var data =  await _reservationDbContext.CandidateExperiences.Where(c => c.IdCandidate == request.idCandidate).Select(c => new CandidateExperienceDTO
        {
           IdCandidateExperience = c.IdCandidateExperience,
            IdCandidate = c.IdCandidate,
            Company = c.Company,
            Job = c.Job,
            Description = c.Description,
            Salary = c.Salary,
            BeginDate = c.BeginDate,
            EndDate = c.EndDate,
            InsertDate = c.InsertDate,
            ModifyDate = c.ModifyDate
        }).ToListAsync(cancellationToken);

        return data;

    }
}

