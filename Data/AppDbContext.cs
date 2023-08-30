using Microsoft.EntityFrameworkCore;
using Moments_Backend.Models;
using Npgsql;

namespace Moments_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Moment> Moments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        private IConfiguration _configuration { get; set; }
        private string _connectionString { get; set; }
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _connectionString = _configuration.GetValue<string>("MomentsDatabase:ConnectionStringDev");

            string connectionString = _connectionString;
            NpgsqlDataSourceBuilder npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            var dataSourceBuilder = npgsqlDataSourceBuilder;
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource);
        }
    }
}
