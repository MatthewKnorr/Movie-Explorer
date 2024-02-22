using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_Explorer.Data;
using Movie_Explorer.Models;

namespace Movie_Explorer.Pages
{
    public class SearchModel : PageModel
    {
        // Inject database and user manager
        private readonly UserManager<CustomUser> _userManager;
        private readonly ApplicationDbContext _context;
        
        public SearchModel(UserManager<CustomUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ICollection<LikedMovie> LikedMovies { get; set; }

        public async Task<IActionResult> AddLikedMovie(string movieTitle)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Add the movie title to the user's liked movies list
            user.LikedMovies ??= new List<LikedMovie>();
            if (!user.LikedMovies.Any(m => m.Title == movieTitle))
            {
                user.LikedMovies.Add(new LikedMovie { Title = movieTitle });
                await _userManager.UpdateAsync(user);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync(string movieTitle)
        {
            if (!string.IsNullOrEmpty(movieTitle))
            {
                return await AddLikedMovie(movieTitle);
            }
            return Page();
        }
    }
}