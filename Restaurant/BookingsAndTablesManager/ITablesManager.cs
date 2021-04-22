using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.BookingsAndTablesManager
{ 
    public interface ITablesManager
    {
        bool IsATableFree(int minNumberOfPersons, DateTime dateTime, Hour hour);
        Table GetFreeTable(int minNumberOfPersons, DateTime dateTime, Hour hour);
        IEnumerable<Table> GetAllTables();
        bool AddTable(Table table);
        bool DeleteTable(int id);
    }
}
