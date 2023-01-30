using Bogus;
using Booking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Data.Data
{
    public class SeedData
    {
        private static IServiceProvider _services { get; set; } = default!;
        private static UserManager<ApplicationUser> _userManager { get; set; } = default!;
        private static RoleManager<IdentityRole> _roleManager { get; set; } = default!;

        public static async Task InitAsync(IServiceProvider services)
        {
            if(services is null) throw new ArgumentNullException(nameof(services));

            _services = services;
            _userManager = _services.GetRequiredService<UserManager<ApplicationUser>>();
            ArgumentNullException.ThrowIfNull(nameof(_userManager));
            _roleManager = _services.GetRequiredService<RoleManager<IdentityRole>>();
            ArgumentNullException.ThrowIfNull(nameof(_roleManager));

            //generate roles
            var adminRole = await NewRoleAsync("Admin");
            var memberRole = await NewRoleAsync("Member");

            //generate members and assign to a role
            var members = GenerateMembers(4);
            await NewUsersAsync(members, memberRole.Name!);

            //generate admins
            //var randomadmins = GenerateMembers(2);
            //await NewUsersAsync(randomadmins, adminRole.Name!);

            //should maybe make NewUserAsync too...??
            var admins = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Email="admin@gymbokning.se",
                    UserName="admin@gymbokning.se",
                    FirstName="Administra",
                    LastName="Tion",
                    EmailConfirmed=true
                }
            };

            //dotnet user-secrets set "AdminPW" "Qwerty!23"
            var config = services.GetRequiredService<IConfiguration>();
            var adminPW = config["AdminPW"];
            ArgumentNullException.ThrowIfNull(adminPW);

            await NewUsersAsync(admins, adminRole.Name!, adminPW);

            //generate classes
            //var db = services.GetRequiredService<ApplicationDbContext>();
            //if (db.GymClasses.Any()) return;
            //var classes = GenerateClasses(10);
        }

        private static List<GymClass> GenerateClasses(int amount)
        {
            Random rnd = new();
            var mins = rnd.Next(1, 5) * 10; //makes same for all somehow...

            var faker = new Faker<GymClass>()
                //.UseSeed(1020)
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.StartTime, f => f.Date.Soon())
                .RuleFor(o => o.Duration, f => new TimeSpan(0, 0, mins, 0))
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                ;

            var fakes = faker.Generate(amount);

            return fakes;
        }

        private static List<ApplicationUser> GenerateMembers(int amount)
        {
            var guid = Guid.NewGuid().ToString();

            var faker = new Faker<ApplicationUser>()
                //.UseSeed(1020)
                //.RuleFor(o => o.Id, f => f.Random.Guid().ToString())
                .RuleFor(o => o.Email, f => f.Internet.Email())
                //.RuleFor(o => o.NormalizedEmail, (f, u) => u.Email.ToUpper())
                .RuleFor(o => o.UserName, (f, u) => u.Email)
                //.RuleFor(o => o.NormalizedUserName, (f, u) => u.Email.ToUpper())
                .RuleFor(o => o.EmailConfirmed, f => true)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                ;

            var fakes = faker.Generate(amount);

            return fakes;
        }

        private static async Task<IdentityRole> NewRoleAsync(string name)
        {
            var roleExists = await _roleManager.FindByNameAsync(name);

            if (roleExists != null)
            {
                return roleExists;
            }
            else
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = name });
                return new IdentityRole();
            }
        }

        private static async Task NewUsersAsync(List<ApplicationUser> users, string role, string password = "")
        {
            foreach (var u in users)
            {
                var user = await _userManager.FindByIdAsync(u.Id);

                if (user == null)
                {
                    var psw = password == "" ? new Faker().Internet.Password() : password;
                    var result = await _userManager.CreateAsync(u, psw);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(u, role);
                    }
                }
            }

        }
    }
}
