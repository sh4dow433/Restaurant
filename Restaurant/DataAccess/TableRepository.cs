using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _dbContext;
        public TableRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Table table)
        {
            if (table == null)
            {
                return false;
            }
            if (_dbContext.Tables.Where(t => t.TablesNumber == table.TablesNumber).Count() < 1)
            {
                _dbContext.Tables.Add(table);
                return true;

            }
            return false;
        }

        public IEnumerable<Table> GetAll()
        {
            return _dbContext.Tables
                .Include(t => t.Bookings)
                .ToList();
        }

        public IEnumerable<Table> GetAllByNumberOfSits(int number)
        {
            return _dbContext.Tables
                .Include(t => t.Bookings)
                .Where(t => t.NumberOfChairs == number)
                .ToList();
        }

        public Table GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _dbContext.Tables
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public int GetMaximumNoOfChairs()
        {
            return _dbContext.Tables.Max(t => t.NumberOfChairs);
        }

        public bool RemoveById(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            var table = _dbContext.Tables
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
            if (table == null)
            {
                return false;
            }
            _dbContext.Tables.Remove(table);
            return true;
        }
    }
}
