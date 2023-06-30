using C969.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class Address
    {
        public Address()
        {
            this.createdBy = ApplicationState.CurrentUser.userName;
            this.createDate = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
            this.lastUpdate = DateTime.Now;
        }
        [Required]
        public int addressId { get; set; }
        [Required]
        public string address { get; set; }
        public string address2 { get; set; }
        [Required]
        public int cityId { get; set; }
        [Required]
        public string postalCode { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public DateTime createDate { get; set; }
        [Required]
        public string createdBy { get; set; }
        [Required]
        public DateTime lastUpdate { get; set; }
        [Required]
        public string lastUpdateBy { get; set; }
        public void UpdateAddress()
        {
            this.lastUpdate = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
        }
    }
}
