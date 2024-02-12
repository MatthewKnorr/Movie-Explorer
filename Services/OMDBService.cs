using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class OMDBService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OMDBService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiKey = "9275f68f"; // OMDB API key
    }

    public async Task<List<MovieInfo>> SearchMoviesAsync(string query)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?s={Uri.EscapeDataString(query)}&apikey={_apiKey}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movieSearchResult = JsonSerializer.Deserialize<MovieSearchResult>(content);

                if (movieSearchResult != null && movieSearchResult.Search != null)
                {
                    var movieInfos = new List<MovieInfo>();

                    foreach (var movie in movieSearchResult.Search)
                    {
                        var detailedResponse = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={movie.imdbID}&apikey={_apiKey}");
                        if (detailedResponse.IsSuccessStatusCode)
                        {
                            var detailedContent = await detailedResponse.Content.ReadAsStringAsync();
                            var movieInfo = JsonSerializer.Deserialize<MovieInfo>(detailedContent);
                            movieInfos.Add(movieInfo);
                        }
                    }

                    return movieInfos;
                }
            }

            // Handle unsuccessful response
            return null;
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}

public class MovieInfo
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Plot { get; set; }
    // Add more properties as needed
}

public class MovieSearchResult
{
    public List<MovieSearchItem> Search { get; set; }
}

public class MovieSearchItem
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string imdbID { get; set; }
    // Add more properties as needed
}