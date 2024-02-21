        // Function to handle form submission and redirect
        function handleSearch(event) {
            event.preventDefault(); // Prevent form submission
            var query = document.getElementById('searchInput').value;
            window.location.href = "/Search?query=" + encodeURIComponent(query);
        }

        // Function to update the display of liked movies
        function updateLikedMoviesDisplay() {
            var likedMoviesContainer = document.getElementById('likedMovies');
            likedMoviesContainer.innerHTML = ''; // Clear existing content

            var likedMovies = JSON.parse(localStorage.getItem('likedMovies')) || [];

            likedMovies.forEach(function (movie) {
                // Create a movie card and append it to the container
                var card = document.createElement('div');
                card.className = 'card mb-3';

                // Create the card body
                var cardBody = document.createElement('div');
                cardBody.className = 'card-body';

                // Title
                var title = document.createElement('h5');
                title.className = 'card-title';
                title.textContent = movie.Title;
                cardBody.appendChild(title);

                // Poster
                var poster = document.createElement('img');
                poster.className = 'card-img-top';
                poster.src = movie.Poster;
                poster.alt = movie.Title + ' Poster';
                cardBody.appendChild(poster);

                // More Info Button
                var moreInfoButton = document.createElement('button');
                moreInfoButton.textContent = 'More Info';
                moreInfoButton.className = 'btn btn-primary';
                moreInfoButton.onclick = function() {
                    window.open('https://www.imdb.com/title/' + movie.imdbID, '_blank');
                };
                cardBody.appendChild(moreInfoButton);

                card.appendChild(cardBody);
                likedMoviesContainer.appendChild(card);
            });
        }

        // Call the function to display liked movies when the page loads
        document.addEventListener('DOMContentLoaded', updateLikedMoviesDisplay);