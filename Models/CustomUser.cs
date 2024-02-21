/*
The CustomUser class has been set up to allow for adding additional fields to the user's profile, and specifically a list of liked movies. It inherits everything from the default IdentityUser and adds to it a LikedMovie list.
*/

using Microsoft.AspNetCore.Identity;

namespace Movie_Explorer.Models;

public class CustomUser : IdentityUser
{
    public ICollection<LikedMovie>? LikedMovies { get; set; }
}

