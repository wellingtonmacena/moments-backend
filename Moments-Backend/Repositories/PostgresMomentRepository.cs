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
        public PostgresMomentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Moment CreateOne(Moment moment)
        {
            _appDbContext.Moments.Add(moment);
            _appDbContext.SaveChanges();

            return moment;
        }
       
        public List<Moment> GetAll()
        {
            List<Moment> moments = _appDbContext.Moments.ToList();

            return moments;
        }

        public Moment GetOne(int id)
        {
            Moment foundMoment = _appDbContext.Moments.ToList().Find(item => item.Id.Equals(id));

            return foundMoment;
        }

        public bool UpdateOne(Moment moment)
        {
            Moment foundMoment = _appDbContext.Moments.First(item => item.Id.Equals(moment.Id));

            if (foundMoment != null)
            {
                foundMoment.Title = moment.Title;
                foundMoment.Description = moment.Description;
                foundMoment.UpdatedAt= DateTime.UtcNow;
                _appDbContext.Moments.Update(foundMoment);
                _appDbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Moment DeleteOne(int id)
        {
            Moment foundMoment = _appDbContext.Moments.ToList().Find(item => item.Id.Equals(id));

            if (foundMoment != null)
            {
                _appDbContext.Moments.Remove(foundMoment);
                _appDbContext.SaveChanges();

                return foundMoment;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteAll()
        {
            _appDbContext.Moments.ExecuteDelete();
            _appDbContext.SaveChanges();
            return true;
        }
    }
}
