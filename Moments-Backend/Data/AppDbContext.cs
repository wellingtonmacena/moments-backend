using Microsoft.EntityFrameworkCore;
using Moments_Backend.Models;
using Npgsql;

namespace Moments_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public IConfiguration _configuration { get; set; }
        public AppDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetValue<string>("MomentsDatabase:ConnectionStringLocal");
            NpgsqlDataSourceBuilder npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            var dataSourceBuilder = npgsqlDataSourceBuilder;
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moment>()
                        .HasMany(e => e.Comments)
                        .WithOne()
                        .HasForeignKey(e => e.MomentId)
                        .IsRequired();

            modelBuilder.Entity<Comment>()
                        .HasOne<Moment>()
                        .WithMany(e => e.Comments)
                        .HasForeignKey(e => e.MomentId)
                        .IsRequired();
        }
    }
}
