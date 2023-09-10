using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moments_Backend.Models;
using Npgsql;

namespace Moments_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public IConfiguration _configuration { get; set; }
        public string Connection { get; set; }
        public AppDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        public AppDbContext(string v)
        {
            Connection = v;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Connection==null?  _configuration.GetValue<string>("MomentsDatabase:ConnectionStringLocal"): Connection;
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
