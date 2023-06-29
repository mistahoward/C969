using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class CustomerMeta
    {
        [Required]
        public int customerId { get; set; }
        [Required]
        public string customerName { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
