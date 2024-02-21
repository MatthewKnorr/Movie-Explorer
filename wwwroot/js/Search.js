// Array to store liked movies
var likedMovies = [];

// Function to like a movie
function likeMovie(movieTitle) {
    // Check if the movie is already liked
    if (!likedMovies.includes(movieTitle)) {
        likedMovies.push(movieTitle);
        // Update liked movies display
        updateLikedMoviesDisplay();
        // Save liked movies to local storage
        saveLikedMovies();
    } else {
        alert("You've already liked this movie!");
    }
}

// Function to update the display of liked movies
function updateLikedMoviesDisplay() {
    var likedMoviesText = "The liked movies are: ";
    for (var i = 0; i < likedMovies.length; i++) {
        likedMoviesText += "\n- " + likedMovies[i];
    }
    document.getElementById("likedMovieText").innerText = likedMoviesText;
}

// Function to save liked movies to local storage
function saveLikedMovies() {
    localStorage.setItem("likedMovies", JSON.stringify(likedMovies));
}

// Function to load liked movies from local storage
function loadLikedMovies() {
    var storedLikedMovies = localStorage.getItem("likedMovies");
    if (storedLikedMovies) {
        likedMovies = JSON.parse(storedLikedMovies);
        updateLikedMoviesDisplay();
    }
}

// Search function
function search() {
    var searchTerm = document.getElementById("searchInput").value;
    var apiKey = "7f78f9aa"; 

    // Make sure search term is not empty
    if (searchTerm.trim() !== "") {
        var url = "http://www.omdbapi.com/?s=" + encodeURIComponent(searchTerm) + "&apikey=" + apiKey;

        // Make AJAX request to OMDB API
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url, true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                displayResults(response);
            }
        };
        xhr.send();
    } else {
        // If search term is empty, clear previous results
        document.getElementById("searchResults").innerHTML = "";
    }
}

// Function to display search results
function displayResults(data) {
    var resultsDiv = document.getElementById("searchResults");
    resultsDiv.innerHTML = "";

    if (data.Response === "True" && data.Search) {
        data.Search.forEach(function (movie) {
            var movieDiv = document.createElement("div");
            movieDiv.classList.add("movie-card");

            var detailsDiv = document.createElement("div");
            detailsDiv.classList.add("movie-info");
            getMovieDetails(movie.imdbID, detailsDiv);
            movieDiv.appendChild(detailsDiv);

            // Button to like the movie
            var likeButton = document.createElement("button");
            likeButton.textContent = "Like ❤️";
            likeButton.classList.add("btn", "btn-primary", "mr-2");
            likeButton.onclick = function () {
                likeMovie(movie.Title);
            };
            movieDiv.appendChild(likeButton);

            // Button for more info
            var moreInfoButton = document.createElement("button");
            moreInfoButton.textContent = "More Info";
            moreInfoButton.classList.add("btn", "btn-secondary");
            moreInfoButton.onclick = function () {
                window.open("https://www.imdb.com/title/" + movie.imdbID, "_blank");
            };
            movieDiv.appendChild(moreInfoButton);

            resultsDiv.appendChild(movieDiv);
        });
    } else {
        resultsDiv.textContent = "No results found";
    }
}

// Function to get movie details
function getMovieDetails(imdbID, detailsDiv) {
    var apiKey = "7f78f9aa"; 
    var url = "http://www.omdbapi.com/?i=" + imdbID + "&apikey=" + apiKey;

    // Make AJAX request to OMDB API to get movie details by IMDb ID
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var movieDetails = JSON.parse(xhr.responseText);
            displayMovieDetails(movieDetails, detailsDiv);
        }
    };
    xhr.send();
}

// Function to display movie details
function displayMovieDetails(details, detailsDiv) {
    var title = document.createElement("h2");
    title.textContent = details.Title;
    detailsDiv.appendChild(title);

    var posterDiv = document.createElement("div");
    posterDiv.classList.add("poster");

    var poster = document.createElement("img");
    poster.src = details.Poster;
    posterDiv.appendChild(poster);
    detailsDiv.appendChild(posterDiv);

    var rated = document.createElement("p");
    rated.textContent = "Rated: " + details.Rated;
    detailsDiv.appendChild(rated);

    var released = document.createElement("p");
    released.textContent = "Released: " + details.Released;
    detailsDiv.appendChild(released);

    var runtime = document.createElement("p");
    runtime.textContent = "Runtime: " + details.Runtime;
    detailsDiv.appendChild(runtime);

    var genre = document.createElement("p");
    genre.textContent = "Genre: " + details.Genre;
    detailsDiv.appendChild(genre);

    var plot = document.createElement("p");
    plot.textContent = "Plot: " + details.Plot.substring(0, 350) + (details.Plot.length > 350 ? "..." : "");
    detailsDiv.appendChild(plot);

    var awards = document.createElement("p");
    awards.textContent = "Awards: " + details.Awards;
    detailsDiv.appendChild(awards);

    var imdbRating = document.createElement("p");
    imdbRating.textContent = "IMDb Rating: " + details.imdbRating;
    detailsDiv.appendChild(imdbRating);

    var metascore = document.createElement("p");
    metascore.textContent = "Metascore: " + details.Metascore;
    detailsDiv.appendChild(metascore);

    var boxOffice = document.createElement("p");
    boxOffice.textContent = "Box Office: " + details.BoxOffice;
    detailsDiv.appendChild(boxOffice);
}

// Load liked movies from local storage on page load
window.onload = loadLikedMovies;
