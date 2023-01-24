using Microsoft.AspNetCore.Identity;

namespace Booking.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> GymClasses { get; set; } = new List<ApplicationUserGymClass>();
    }
}
