﻿using C969.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Models
{
    public class Appointment
    {
        public Appointment()
        {
            this.createdBy = ApplicationState.CurrentUser.userName;
            this.createDate = DateTime.Now;
            this.start = DateTime.Now;
            this.end = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
            this.lastUpdate = DateTime.Now;
            this.userId = ApplicationState.CurrentUser.userId;
        }
        [Required]
        public int userId { get; set; }
        [Required]
        public int appointmentId { get; set; }
        [Required]
        public int customerId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        public DateTime start { get; set; }
        [Required]
        public DateTime end { get; set; }
        [Required]
        public DateTime createDate { get; set; }
        [Required]
        public string createdBy { get; set; }
        [Required]
        public DateTime lastUpdate { get; set; }
        [Required]
        public string lastUpdateBy { get; set; }
        public void UpdateAppointment()
        {
            this.lastUpdate = DateTime.Now;
            this.lastUpdateBy = ApplicationState.CurrentUser.userName;
        }
    }
}
