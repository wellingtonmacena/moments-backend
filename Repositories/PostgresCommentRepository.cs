using Moments_Backend.Data;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Repositories
{
    public class PostgresCommentRepository: ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        //Func<Transaction, Transaction> WhereEmpty = (x) => x;
        public PostgresCommentRepository(IConfiguration configuration)
        {
            _appDbContext = new AppDbContext(configuration);
        }

        public bool CreateOne(Comment moment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOne(Comment moment)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne(Comment moment)
        {
            throw new NotImplementedException();
        }
    }
}
