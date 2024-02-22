using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_Explorer.Data;
using Movie_Explorer.Models;

public class AddLikedMovieModel : PageModel
{
    // Inject usermanager
    private readonly UserManager<CustomUser> _userManager;
    public AddLikedMovieModel(UserManager<CustomUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnPostAsync(string movieTitle)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound("Please log in before trying to use this feature.");
        }

        // Add the movie title to the user's liked movies list
        user.LikedMovies ??= new List<LikedMovie>();
        if (!user.LikedMovies.Any(m => m.Title == movieTitle))
        {
            user.LikedMovies.Add(new LikedMovie { Title = movieTitle });
            await _userManager.UpdateAsync(user);
        }
        // Return a success response
        return StatusCode(200);
    }
}
