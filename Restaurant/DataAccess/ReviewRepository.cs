using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _dbContext;

        public ReviewRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Review review)
        {
            if (review == null)
            {
                return false;
            }
            _dbContext.Reviews.Add(review);
            return true;
        }

        public IEnumerable<Review> GetAllOrderedDesc()
        {
            return _dbContext.Reviews
                 .OrderByDescending(r => r.Date)
                 .ToList();
        }

        public IEnumerable<Review> GetIntervalOrderedDesc(int pageNumber)
        {
            int numberOfReviewsToSkip = pageNumber * 10;
            int numberOfReviewsToTake = pageNumber * 10 + 10;
            return _dbContext.Reviews
                .OrderByDescending(r => r.Date)
                .Skip(numberOfReviewsToSkip)
                .Take(numberOfReviewsToTake)
                .ToList();
        }

        public int GetNumberOfPages()
        {
            return (_dbContext.Reviews.Count()-1)/10;
        }


        public bool RemoveById(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            var review = _dbContext.Reviews
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
            if (review == null)
            {
                return false;
            }
            _dbContext.Reviews.Remove(review);
            return true;
        }
    }
}
