using Microsoft.EntityFrameworkCore;
using Moments_Backend.Data;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Repositories
{
    public class PostgresCommentRepository: ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        public PostgresCommentRepository(IConfiguration configuration)
        {
            _appDbContext = new LocalPostgresContext(configuration);
        }

        public Comment CreateOne(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
            _appDbContext.SaveChanges();

            return comment;
        }

        public bool DeleteOne(Comment comment)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllByMomentId(string momentId)
        {
            List<Comment> comments = _appDbContext.Comments
                                                  .ToList()
                                                  .Where(item => item.MomentId.Equals(momentId)).ToList();
            return comments;
        }

        public Comment GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
