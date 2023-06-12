﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    class Address
    {
        [Required]
        public int addressId { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
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
    }
}
