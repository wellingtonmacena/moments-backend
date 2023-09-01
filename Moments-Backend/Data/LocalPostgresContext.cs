using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Moments_Backend.Data
{
    public class LocalPostgresContext: AppDbContext
    {
        public LocalPostgresContext(IConfiguration configuration)
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
    }
}
