using Microsoft.EntityFrameworkCore;
using Moments_Backend.Data;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Repositories
{
    public class PostgresMomentRepository : IMomentRepository
    {
        private readonly AppDbContext _appDbContext;
        //Func<Transaction, Transaction> WhereEmpty = (x) => x;
        public PostgresMomentRepository(IConfiguration configuration)
        {
            _appDbContext = new AppDbContext(configuration);
        }

        public bool CreateOne(Moment moment)
        {
            _appDbContext.Moments.Add(moment);
            //_appDbContext.SaveChanges();

            return true;
        }

        public bool DeleteOne(Moment moment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll()
        {
            _appDbContext.Moments.ExecuteDelete();
            _appDbContext.SaveChanges();
            return true;
        }

        public List<Moment> GetAll()
        {
            List<Moment> moments = _appDbContext.Moments.ToList();

            return moments;
        }

        public Moment GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOne(Moment moment)
        {
            throw new NotImplementedException();
        }
    }
}
