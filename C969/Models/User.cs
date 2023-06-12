using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class User
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public bool active { get; set; }
        [Required]
        public DateTime createDate { get; set; }
        [Required]
        public string createdBy { get; set; }
        [Required]
        public DateTime lastUpdate { get; set; }
        [Required]
        public string lastUpdateBy { get; set; }

    }
}
