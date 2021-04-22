using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    [NotMapped]
    public class Admin
    {
        public string Username { get; set; }

        public string Password { get; set; }

    }
}
