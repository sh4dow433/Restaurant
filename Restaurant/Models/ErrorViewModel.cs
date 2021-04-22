using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    [NotMapped]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
