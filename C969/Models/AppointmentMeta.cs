using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class AppointmentMeta
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public DateTime start { get; set; }
        [Required]
        public DateTime end { get; set; }
    }
}
