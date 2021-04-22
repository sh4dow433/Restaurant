using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.BookingsAndTablesManager
{
    public interface IBookingsManager
    {
        Booking MakeBooking(CreateBookingModel booking);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetAllActiveBookings();
        Booking GetBookingById(int id);
        bool RemoveBookingById(int id);
    }
}
