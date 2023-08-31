using Microsoft.EntityFrameworkCore;
using Moments_Backend.Models;

namespace Moments_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public IConfiguration _configuration { get; set; }

    }
}
