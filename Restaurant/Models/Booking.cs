using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public enum Hour
    {
        h18,
        h19_30,
        h21,
        h22_30,
        h24
    }
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(60)]
        public string Email { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public bool Active { get => DateTime.Compare(DateTime, DateTime.Now.AddDays(-1)) > 0; } 
        [Required]
        public Table Table { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public Hour Hour { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
