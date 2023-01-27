using Bogus.DataSets;
using Booking.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace Booking.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<GymClass> GymClasses { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            //string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            ////seed admin role
            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Name = "Admin",
            //    NormalizedName = "ADMIN",
            //    Id = ROLE_ID,
            //    ConcurrencyStamp = ROLE_ID
            //});

            ////create user
            //var appUser = new ApplicationUser
            //{
            //    Id = ADMIN_ID,
            //    Email = "admin@admin.com",
            //    EmailConfirmed = true,
            //    FirstName = "Amin",
            //    LastName = "Adin",
            //    UserName = "admin@admin.com",
            // NormalizedUserName = "ADMIN@ADMIN.COM"
            //};

            ////set user password
            //PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            //appUser.PasswordHash = ph.HashPassword(appUser, "mypassword_ ?");

            ////seed user
            //builder.Entity<ApplicationUser>().HasData(appUser);

            ////set user role to admin
            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = ROLE_ID,
            //    UserId = ADMIN_ID
            //});
            builder.Entity<ApplicationUser>().Property<DateTime>("TimeOfRegistration");
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserGymClass>().HasKey(a => new { a.ApplicationUserId, a.GymClassId });
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ApplicationUser>().Where(e => e.State == EntityState.Added))
            {
                entry.Property("TimeOfRegistration").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}