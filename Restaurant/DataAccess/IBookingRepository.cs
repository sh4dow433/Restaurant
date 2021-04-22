using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public interface IBookingRepository
    {
        public Booking GetById(int id);
        public IEnumerable<Booking> GetAllOrderedByDate();
        public bool Add(Booking booking);
        public bool RemoveById(int id);
    }
}
