using Movie_Explorer.Data;
namespace Movie_Explorer.Models;

public class GetLikedMovieList : IUserService
{
    private readonly ApplicationDbContext _context;

    public GetLikedMovieList(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICollection<LikedMovie>? GetLikedMovies(string emailAddress)
    {
        // Query the database to retrieve the user by email address
        var user = _context.Users.First(u => u.Email == emailAddress);

        // If the user is found, return the LikedMovies list
        if (user != null)
        {
            return user.LikedMovies;
        }

        else
        {
            // If the user is not found or LikedMovies is null, return an empty list
            return null;
        }
    }
}