using Restaurant.DataAccess;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.BookingsAndTablesManager
{
    public class BookingsManager : IBookingsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITablesManager _tablesManager;

        public BookingsManager(IUnitOfWork unitOfWork, ITablesManager tablesManager)
        {
            _unitOfWork = unitOfWork;
            _tablesManager = tablesManager;
        }

        public IEnumerable<Booking> GetAllActiveBookings()
        {
            return _unitOfWork.Bookings.GetAllOrderedByDate().Where(b => b.Active == true).ToList();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _unitOfWork.Bookings.GetAllOrderedByDate();
        }

        public Booking GetBookingById(int id)
        {
            return _unitOfWork.Bookings.GetById(id);
        }

        public Booking MakeBooking(CreateBookingModel createBookingModel)
        {
            var table = _tablesManager.GetFreeTable(createBookingModel.NumberOfGuests, createBookingModel.Date, createBookingModel.Hour);
          
            if (table == null)
                return null;

            Booking booking = new Booking
            {
                FirstName = createBookingModel.FirstName,
                LastName = createBookingModel.LastName,
                Email = createBookingModel.Email,
                NumberOfGuests = createBookingModel.NumberOfGuests,
                PhoneNumber = createBookingModel.PhoneNumber,
                DateTime = createBookingModel.Date,
                Table = table,
                Hour = createBookingModel.Hour
            };

            _unitOfWork.Bookings.Add(booking);
            _unitOfWork.SaveChanges();
            return booking;
        }

        public bool RemoveBookingById(int id)
        {
            if(_unitOfWork.Bookings.RemoveById(id))
            {
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
