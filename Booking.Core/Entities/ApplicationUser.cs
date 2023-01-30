using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace Booking.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Name")]
        public string FullName => FirstName + " " + LastName;

        public ICollection<ApplicationUserGymClass> GymClasses { get; set; } = new List<ApplicationUserGymClass>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userIdentity.AddClaim(new Claim("FullName", this.FullName));
            return userIdentity;
        }
    }
}
