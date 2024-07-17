using Microsoft.EntityFrameworkCore;
using Model.Domain;

namespace Repository.connectionUtils
{
    public class TriatlonDBContext : DbContext
    {
        public TriatlonDBContext(DbContextOptions<TriatlonDBContext> options) : base(options)
        {
        }

        public DbSet<Referee> Referees { get; set; }
        public DbSet<Trial> Trials { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the id property to be auto-incremented
            modelBuilder.Entity<Referee>()
                .Property(r => r.id)
                .ValueGeneratedOnAdd();

            // Configure one-to-one relationship between Referee and Trial
            modelBuilder.Entity<Referee>()
                .HasOne(r => r.trial)
                .WithOne(t => t.referee)
                .HasForeignKey<Trial>(t => t.referee_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trial>()
                .HasOne(t => t.referee)
                .WithOne(r => r.trial)
                .HasForeignKey<Referee>(r => r.id);

            // Configure many-to-many relationship between Participant and Trial using Result as the join entity
            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.ParticipantId, r.TrialId }); // Composite key

            modelBuilder.Entity<Result>()
                .HasOne(r => r.participant)
                .WithMany(p => p.Results)
                .HasForeignKey(r => r.ParticipantId);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.trial)
                .WithMany(t => t.Results)
                .HasForeignKey(r => r.TrialId);

            base.OnModelCreating(modelBuilder);
        }
    }
}