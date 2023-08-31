namespace Moments_Backend.Data
{
    public class AWSPostgresContext: AppDbContext
    {
        public AWSPostgresContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
