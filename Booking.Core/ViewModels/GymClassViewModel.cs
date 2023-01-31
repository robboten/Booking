using Booking.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booking.Core.ViewModels
{
    public class GymClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Starting")]
        public DateTime StartTime { get; set; }
        public TimeSpan Duration {  get; set;  }
        public bool Attending { get; set; }
    }
}
