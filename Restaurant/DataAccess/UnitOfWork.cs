using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IBookingRepository Bookings { get; private set; }

        public ITableRepository Tables { get; private set; }

        public IReviewRepository Reviews { get; private set; }


        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            Bookings = new BookingRepository(_dbContext);
            Tables = new TableRepository(_dbContext);
            Reviews = new ReviewRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
