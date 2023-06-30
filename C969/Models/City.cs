using C969.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class City
    {
        public City()
        {
            this.createdBy = ApplicationState.CurrentUser.userName;
            this.createDate = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
            this.lastUpdate = DateTime.Now;
        }

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
        public void UpdateCity()
        {
            this.lastUpdate = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
        }
    }
}
