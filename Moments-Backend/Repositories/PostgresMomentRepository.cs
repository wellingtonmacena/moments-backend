using Microsoft.EntityFrameworkCore;
using Moments_Backend.Data;
using Moments_Backend.Models;
using Moments_Backend.Repositories.Interfaces;

namespace Moments_Backend.Repositories
{
    public class PostgresMomentRepository : IMomentRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostgresMomentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateOne(Moment moment)
        {
            _appDbContext.Moments.Add(moment);
            await _appDbContext.SaveChangesAsync();
        }

        public List<Moment> GetAll()
        {
            List<Moment> moments = _appDbContext.Moments
                                                .Select(item => new Moment(item.Id, item.Title, item.Description, item.ImageURL, item.CreatedAt, item.Comments))
                                                .ToList();

            return moments;
        }

        public Moment GetOne(int id)
        {
            Moment foundMoment = _appDbContext.Moments
                                  .First(item => item.Id.Equals(id));

            return foundMoment;
        }

        public bool UpdateOne(Moment moment)
        {
            Moment foundMoment = _appDbContext
                                  .Moments
                                  .First(item => item.Id.Equals(moment.Id));

            if (foundMoment != null)
            {
                foundMoment.Title = moment.Title;
                foundMoment.Description = moment.Description;
                foundMoment.UpdatedAt = DateTime.UtcNow;
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
            Moment foundMoment = _appDbContext
                                  .Moments
                                  .First(item => item.Id.Equals(id));

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

        public async Task CreateOneComment(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateMany(List<Moment> moments)
        {
            _appDbContext.Moments.AddRangeAsync(moments);
            await _appDbContext.SaveChangesAsync();

           
        }
    }
}