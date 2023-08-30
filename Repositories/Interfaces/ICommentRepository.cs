using Moments_Backend.Models;

namespace Moments_Backend.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetOne(string id);
        List<Comment> GetAll();
        bool CreateOne(Comment moment);
        bool UpdateOne(Comment moment);
        bool DeleteOne(Comment moment);
    }
}
