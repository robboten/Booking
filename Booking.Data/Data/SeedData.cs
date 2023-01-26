using Bogus;
using Booking.Core.Entities;
using Booking.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static System.Formats.Asn1.AsnWriter;

namespace Booking.Data.Data
{
    public class SeedData
    {
        private IServiceProvider _services { get; set; }
        public static async Task InitAsync(IServiceProvider services)
        {
            //_services = services;

            //generate roles
            var adminRole = await NewRoleAsync(services, "Admin");
            var memberRole= await NewRoleAsync(services, "Member");

            //generate members and assign to a role
            var members = GenerateMembers(4);
            await NewUsersAsync(services, members, memberRole.Name!);

            //? admin@Gymbokning.se ?
            var admins = GenerateMembers(2);
            await NewUsersAsync(services, admins, adminRole.Name!);

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
                //.RuleFor(o => o.Duration, f => f.Date.Timespan())
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

        //private static async Task NewRoleAsync(IServiceProvider services, string name)
        //{
        //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //    var roleExists = await roleManager.RoleExistsAsync(name);
        //    if (!roleExists)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole() { Name = name });
        //    }
        //}

        private static async Task<IdentityRole> NewRoleAsync(IServiceProvider services, string name)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var roleExists = await roleManager.FindByNameAsync(name);

            if (roleExists!=null)
            {
                return roleExists;
            }
            else
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = name });
                return new IdentityRole();
            }
            
        }

        //private static async Task NewUsers(IServiceProvider services, List<ApplicationUser> users)
        private static async Task NewUsersAsync(IServiceProvider services, List<ApplicationUser> users, string role)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var u in users)
            {
                var user = await userManager.FindByIdAsync(u.Id);
                var psw = new Faker().Internet.Password();

                if (user == null)
                {
                    var result = await userManager.CreateAsync(u,psw);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(u, role);
                    }
                }
            }
            
        }
        //private static IdentityRole IdentityRole()
        //{
        //    var guid = Guid.NewGuid().ToString();
        //    var faker = new Faker<IdentityRole>()
        //    .UseSeed(1020)
        //    .RuleFor(o => o.Name, f => f.Lorem.Word())
        //    .RuleFor(o => o.NormalizedName, (f, u) => u.Name.ToUpper())
        //    .RuleFor(o => o.Id, f => guid)
        //    .RuleFor(o => o.ConcurrencyStamp, f => guid)
        //    ;

        //    var fakes = faker.Generate();

        //    return fakes;
        //}


    }
}
