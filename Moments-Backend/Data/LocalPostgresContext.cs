using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Moments_Backend.Data
{
    public class LocalPostgresContext: AppDbContext
    {
        public LocalPostgresContext(IConfiguration configuration):base(configuration)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
