using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.DataAccess
{
    public interface ITableRepository
    {
        public Table GetById(int id);
        public IEnumerable<Table> GetAll();
        public IEnumerable<Table> GetAllByNumberOfSits(int number);
        public bool Add(Table table);
        public bool RemoveById(int id);
        public int GetMaximumNoOfChairs();
    }
}
