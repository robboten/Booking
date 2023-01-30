using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Booking.Core.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Starting")]
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; 
        }
        [Display(Name = "Ending")]
        public DateTime EndTime => StartTime + Duration;
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Attending")]
        public ICollection<ApplicationUserGymClass> AttendingMembers { get; set; } = new List<ApplicationUserGymClass>();

    }
}
