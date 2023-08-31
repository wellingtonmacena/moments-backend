namespace Moments_Backend.Data
{
    public class LocalPostgresContext: AppDbContext
    {
        public LocalPostgresContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
