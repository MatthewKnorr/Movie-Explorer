using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Movie_Explorer.Models;

namespace Movie_Explorer.Data;

public static class SeedData
{
    // Initialize the DB
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext (
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any users.
            if (context.CustomUsers.Any())
            {
                return;   // DB has been seeded
            }
            
            var user = new CustomUser{
                Email = "b@c.com",
                LikedMovies = new List<LikedMovie>()
            };

            var likedMovie = new LikedMovie
            {
                Title = "Jaws"
            };

            user.LikedMovies.Add(likedMovie);

            context.CustomUsers.Add(user);
            context.SaveChanges();
        }
    }
}