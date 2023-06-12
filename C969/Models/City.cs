using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    class City
    {
        [Required]
        public int cityId { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public int countryId { get; set; }
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
