using Reservation_System.Domain;

namespace Reservation_System.Application.DTOs
{
    public class CandidateDTO
    {
        public int IdCandidate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Email { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public List<CandidateExperience>? Experience { get; set; }
    }
}
