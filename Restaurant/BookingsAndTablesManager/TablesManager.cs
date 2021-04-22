using Restaurant.DataAccess;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.BookingsAndTablesManager
{
    public class TablesManager : ITablesManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public TablesManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddTable(Table table)
        {
            if (_unitOfWork.Tables.Add(table))
            {
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteTable(int id)
        {
            if(_unitOfWork.Tables.RemoveById(id))
            {
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Table> GetAllTables()
        {
            return _unitOfWork.Tables.GetAll();
        }

        public Table GetFreeTable(int minNumberOfPersons, DateTime dateTime, Hour hour)
        {
            int maxNum = _unitOfWork.Tables.GetMaximumNoOfChairs();
            for(int num = minNumberOfPersons; num <= maxNum; num++)
            {
                var tables = _unitOfWork.Tables.GetAllByNumberOfSits(num);
                if (tables.Count() == 0)
                    continue;
                foreach (var table in tables)
                {
                    var auxTable = table.Bookings.Where(b => b.DateTime == dateTime && b.Hour == hour)
                        .Select(b => b.Table)
                        .FirstOrDefault();
                    if (auxTable == null)
                    {
                        return table;
                    }
                }
                num++;
            }
            return null;
        }

        public bool IsATableFree(int minNumberOfPersons, DateTime dateTime, Hour hour)
        {
            int num = minNumberOfPersons;
            int maxNum = _unitOfWork.Tables.GetMaximumNoOfChairs();
            bool flag = false;

            while (!flag)
            {
                var tables = _unitOfWork.Tables.GetAllByNumberOfSits(num);
                if (tables.Count() == 0)
                    break;
                foreach (var table in tables)
                {
                    var freeTable = table.Bookings.Where(b => b.DateTime == dateTime && b.Hour == hour).Select(b => b.Table).FirstOrDefault();
                    if (freeTable == null)
                    {
                        flag = true;
                    }
                }
                num++;
                if (num > maxNum)
                    break;
            }
            return flag;
        }
    }
}
