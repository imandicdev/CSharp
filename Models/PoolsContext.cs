using Microsoft.EntityFrameworkCore;

namespace Pools.Models
{
    public class PoolsContext : DbContext
    {
        public PoolsContext(DbContextOptions<PoolsContext> options)
            :base(options)
        { }

        public DbSet<Pools> Pools { get; set; }
        public DbSet<Sessions> Sessions { get; set; }

        public DbSet<Reservations> Reservations { get; set; }
    }
}
