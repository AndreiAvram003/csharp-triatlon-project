using Microsoft.EntityFrameworkCore;

namespace Repository.connectionUtils;

public class TriatlonDBContext : DbContext
{
    public TriatlonDBContext(DbContextOptions<TriatlonDBContext> options) : base(options)
    {
    }
    
    public DbSet<Model.Domain.Participant> Participants { get; set; }
}
