using EventBooking.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.MigrateAsync();

        // ==========================
        // Seed Roles
        // ==========================

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(
                new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(
                new IdentityRole("User"));
        }

        // ==========================
        // Seed Admin User
        // ==========================

        var adminEmail = "admin@test.com";

        var adminUser =
            await userManager
                .FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var user = new IdentityUser
            {
                UserName = "admin",
                Email = adminEmail
            };

            var result =
                await userManager
                    .CreateAsync(
                        user,
                        "Admin@123");

            if (result.Succeeded)
            {
                await userManager
                    .AddToRoleAsync(user, "Admin");
            }
        }

        // ==========================
        // Seed Events
        // ==========================

        if (await context.Events.AnyAsync())
            return;

        var events = new List<Event>
        {
            new Event
            {
                Title = "Tech Conference 2026",
                Description = "Annual tech conference",
                Date = DateTime.UtcNow.AddDays(10),
                Location = "Delhi",
                AvailableSeats = 150
            },

            new Event
            {
                Title = "Music Festival Night",
                Description = "Live music show",
                Date = DateTime.UtcNow.AddDays(15),
                Location = "Mumbai",
                AvailableSeats = 200
            },

            new Event
            {
                Title = "AI Workshop",
                Description = "Hands-on AI session",
                Date = DateTime.UtcNow.AddDays(20),
                Location = "Bangalore",
                AvailableSeats = 100
            }
        };

        await context.Events.AddRangeAsync(events);

        await context.SaveChangesAsync();
    }
}