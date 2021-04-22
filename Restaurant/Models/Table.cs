using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TablesNumber { get; set; }
        [Required]
        public int NumberOfChairs { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
