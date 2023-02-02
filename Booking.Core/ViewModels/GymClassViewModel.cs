using System.ComponentModel.DataAnnotations;

namespace Booking.Core.ViewModels
{
    public class GymClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Starting")]
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Attending { get; set; }
    }
}
