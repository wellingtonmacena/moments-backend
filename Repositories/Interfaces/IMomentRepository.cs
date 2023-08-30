using Moments_Backend.Models;

namespace Moments_Backend.Repositories.Interfaces
{
    public interface IMomentRepository
    {
        Moment GetOne(string id);
        List<Moment> GetAll();
        bool CreateOne(Moment moment);
        bool UpdateOne(Moment moment);
        bool DeleteOne(Moment moment);
        bool DeleteAll();
    }
}
