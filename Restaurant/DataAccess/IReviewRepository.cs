using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetAllOrderedDesc();
        public IEnumerable<Review> GetIntervalOrderedDesc(int page);
        public int GetNumberOfPages();
        public bool Add(Review review);
        public bool RemoveById(int id);
    }
}
