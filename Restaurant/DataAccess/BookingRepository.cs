using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public BookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(Booking booking)
        {
            if (booking == null)
            {
                return false;
            }
            _dbContext.Bookings.Add(booking);
            return true;
        }

        public IEnumerable<Booking> GetAllOrderedByDate()
        {
            return _dbContext.Bookings
                .Include(b => b.Table)
                .OrderByDescending(b => b.DateTime)
                .ToList();
        }

        public Booking GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return _dbContext.Bookings
                .Include(b => b.Table)
                .FirstOrDefault(b => b.Id == id);
        }

        public bool RemoveById(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            var booking = _dbContext.Bookings
                .Where(b => b.Id == id)
                .FirstOrDefault();
            if (booking == null)
            {
                return false;
            }
            _dbContext.Bookings.Remove(booking);
            return true;
        }
    }
}
