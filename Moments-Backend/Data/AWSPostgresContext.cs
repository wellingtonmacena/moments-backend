using Microsoft.EntityFrameworkCore;
using Moments_Backend.Models;
using Npgsql;

namespace Moments_Backend.Data
{
    public class AWSPostgresContext: AppDbContext
    {
        public AWSPostgresContext(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            string connectionString = _configuration.GetValue<string>("MomentsDatabase:ConnectionStringAWS");
            NpgsqlDataSourceBuilder npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            var dataSourceBuilder = npgsqlDataSourceBuilder;
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
