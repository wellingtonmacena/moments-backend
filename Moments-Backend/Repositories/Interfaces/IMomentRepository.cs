using Moments_Backend.Models;

namespace Moments_Backend.Repositories.Interfaces
{
    public interface IMomentRepository
    {
        Moment GetOne(int id);
        List<Moment> GetAll();
        List<Comment> GetCommentsByMomentId(string id);
        Task CreateOne(Moment moment);
        bool UpdateOne(Moment moment);
        Moment DeleteOne(int id);
        bool DeleteAll();
        Task CreateOneComment(Comment comment);
    }
}
