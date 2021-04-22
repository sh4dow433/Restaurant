using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    [NotMapped]
    public class CreateBookingModel
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
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Hour Hour { get; set; }

    }
}
