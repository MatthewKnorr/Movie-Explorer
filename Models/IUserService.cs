using Movie_Explorer.Models;

public interface IUserService
{
    ICollection<LikedMovie> GetLikedMovies(string emailAddress);
}