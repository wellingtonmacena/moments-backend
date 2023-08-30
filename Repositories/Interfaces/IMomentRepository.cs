using Moments_Backend.Models;

namespace Moments_Backend.Repositories.Interfaces
{
    public interface IMomentRepository
    {
        Moment GetOne(int id);
        List<Moment> GetAll();
        Moment CreateOne(Moment moment);
        bool UpdateOne(Moment moment);
        bool DeleteOne(int id);
        bool DeleteAll();
    }
}
