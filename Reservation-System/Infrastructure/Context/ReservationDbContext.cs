using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Reservation_System.Domain;

namespace Reservation_System.Infrastructure.Context
{
    public class ReservationDbContext : DbContext
    {
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateExperience> CandidateExperiences { get; set; }
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateExperience>(entity =>
            {
                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Job)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(c => c.Salary)
                    .IsRequired()
                    .HasPrecision(8, 2);

                entity.HasKey(e => e.IdCandidateExperience);

            });


            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Surname)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasAlternateKey(c => c.Email);

                entity.HasKey(c => c.IdCandidate);
                entity.HasMany(c => c.Experience)
                      .WithOne(e => e.Candidate)
                      .HasForeignKey(e => e.IdCandidate);
            });
        }

    }

}
