// Function to add a movie to the liked movies list
function likeMovie(movieTitle) {
    var likedMovies = JSON.parse(localStorage.getItem("likedMovies")) || []; // Get liked movies from storage
    if (!likedMovies.includes(movieTitle)) {
        if (likedMovies.length >= 10) {
            // Remove the oldest movie
            likedMovies.shift();
        }
        likedMovies.push(movieTitle); // Add the movie title to the list
        localStorage.setItem("likedMovies", JSON.stringify(likedMovies)); // Save liked movies to local storage
        updateLikedMoviesDisplay(likedMovies); // Update liked movies display
        updateLikedMoviesCount(likedMovies); // Update liked movies count
    } else {
        // Only display alert if the movie is being added by the user
        if (event && event.type === "click") {
            alert("You've already liked this movie!");
        }
    }
}

// Function to remove a movie from the liked movies list
function removeMovie(index) {
    var likedMovies = JSON.parse(localStorage.getItem("likedMovies")) || [];
    likedMovies.splice(index, 1); // Remove the movie at the specified index
    localStorage.setItem("likedMovies", JSON.stringify(likedMovies)); // Save the updated liked movies to local storage
    updateLikedMoviesDisplay(likedMovies); // Update liked movies display
    updateLikedMoviesCount(likedMovies); // Update liked movies count
}

// Function to update the display of liked movies
function updateLikedMoviesDisplay(likedMovies) {
    var likedMoviesList = document.getElementById('likedMoviesList');
    likedMoviesList.innerHTML = ''; // Clear existing content

    likedMovies.forEach(function (movie, index) {
        // Create a list item for each liked movie
        var listItem = document.createElement('li');
        listItem.textContent = movie;

        // Create a remove button for each liked movie
        var removeButton = document.createElement('button');
        removeButton.textContent = 'X';
        removeButton.classList.add('remove-button');
        removeButton.onclick = function () {
            removeMovie(index);
        };

        // Append the remove button to the list item
        listItem.appendChild(removeButton);

        // Append the list item to the list
        likedMoviesList.appendChild(listItem);
    });
}

// Function to update the display of liked movies count
function updateLikedMoviesCount(likedMovies) {
    var likedMoviesCount = likedMovies.length;
    document.getElementById('likedMoviesCount').textContent = likedMoviesCount + "/10 movies"; // Display the count of liked movies
}

// Call this function when the page is loaded to load and display liked movies
document.addEventListener('DOMContentLoaded', function () {
    var likedMovies = JSON.parse(localStorage.getItem("likedMovies")) || []; // Get liked movies from storage
    updateLikedMoviesDisplay(likedMovies); // Update liked movies display
    updateLikedMoviesCount(likedMovies); // Update liked movies count
});

// Test: Add a test movie to the liked movies list
likeMovie("Test Movie");