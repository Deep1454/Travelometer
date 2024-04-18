using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travelometer.Models.Domain;

namespace Travelometer.Areas.Identity.Data
{
    public class TravelometerDbContext : IdentityDbContext<TravelometerUser>
    {
        public TravelometerDbContext(DbContextOptions<TravelometerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<GuestUser> GuestUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Flight>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Hotel>()
                .Property(h => h.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<CarRental>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(builder);

            SeedRoles(builder);
            SeedAdminUser(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "admin" },
                new IdentityRole { Id = "2", Name = "Traveller", NormalizedName = "traveller" }
            );
        }

        private static void SeedAdminUser(ModelBuilder builder)
        {
            // Seed admin user with hashed password (replace with strong password)
            var passwordHasher = new PasswordHasher<TravelometerUser>();
            var adminUser = new TravelometerUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL>COM",
                EmailConfirmed = true, // Set to true if you don't require email confirmation
                LockoutEnabled = true
            };

            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123"); // Replace with strong password

            builder.Entity<TravelometerUser>().HasData(adminUser);

            // Assign admin role to admin user
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1", // Admin role ID
                UserId = adminUser.Id
            });
        }
    }
}
