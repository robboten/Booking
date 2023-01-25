using Bogus;
using Booking.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(ApplicationDbContext db)
        {
            if (await db.GymClasses.AnyAsync()) return;

            var classes = GenerateClasses(10);
            var members = GenerateMembers(10);
            var role = IdentityRole();

            db.AddRange(role);
            db.AddRange(classes);
            db.AddRange(members);
            await db.SaveChangesAsync();
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
                .RuleFor(o => o.Id, f => f.Random.Guid().ToString())
                .RuleFor(o => o.Email, f => f.Internet.Email())
                .RuleFor(o => o.NormalizedEmail, (f, u) => u.Email.ToUpper())
                .RuleFor(o => o.UserName, (f, u) => u.Email)
                .RuleFor(o => o.NormalizedUserName, (f, u) => u.Email.ToUpper())
                .RuleFor(o => o.EmailConfirmed, f => true)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                ;

            var fakes = faker.Generate(amount);

            return fakes;
        }
        private static IdentityRole IdentityRole()
        {
            var guid = Guid.NewGuid().ToString();
            var faker = new Faker<IdentityRole>()
            .UseSeed(1020)
            .RuleFor(o => o.Name, f => f.Lorem.Word())
            .RuleFor(o => o.NormalizedName, (f, u) => u.Name.ToUpper())
            .RuleFor(o => o.Id, f => guid)
            .RuleFor(o => o.ConcurrencyStamp, f => guid)
            ;

            var fakes = faker.Generate();

            return fakes;
        }


    }
}
