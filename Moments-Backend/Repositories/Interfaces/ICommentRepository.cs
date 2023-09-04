using Moments_Backend.Models;

namespace Moments_Backend.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetOne(string id);
        List<Comment> GetAllByMomentId(string id);
        Comment CreateOne(Comment comment);
        bool UpdateOne(Comment comment);
        bool DeleteOne(Comment comment);
    }
}
