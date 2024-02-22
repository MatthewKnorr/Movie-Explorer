using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_Explorer.Data;
using Movie_Explorer.Models;

namespace Movie_Explorer.Pages
{
    public class IndexModel : PageModel
    {
        // Inject database and user manager
        private readonly UserManager<CustomUser> _userManager;
        private readonly ApplicationDbContext _context;
        
        public IndexModel(UserManager<CustomUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ICollection<LikedMovie> LikedMovies { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Page();
            }

            user = await _context.Users
                .Include(u => u.LikedMovies)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            
            LikedMovies = user.LikedMovies;

            return Page();
        } 
    }
}