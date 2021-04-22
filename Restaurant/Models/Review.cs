using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
