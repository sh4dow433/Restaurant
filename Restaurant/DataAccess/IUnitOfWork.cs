using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        public IBookingRepository Bookings { get; }
        public ITableRepository Tables { get; }
        public IReviewRepository Reviews { get; }

        int SaveChanges();
    }
}
