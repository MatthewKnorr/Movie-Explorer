using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class MovieInfo
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Plot { get; set; }
    public string Rated { get; set; }
    public string Runtime { get; set; }
    public string Awards { get; set; }
    public string BoxOffice { get; set; }
    public string Poster { get; set; }
    public string Metascore { get; set; }
    public string ImdbRating { get; set; }
    public List<MovieRating> Ratings { get; set; }
}

public class MovieRating
{
    public string Source { get; set; }
    public string Value { get; set; }
}

public class OMDBService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OMDBService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
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
                        var detailedResponse = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={movie.imdbID}&apikey={_apiKey}&plot=short&r=json");
                        if (detailedResponse.IsSuccessStatusCode)
                        {
                            var detailedContent = await detailedResponse.Content.ReadAsStringAsync();
                            var detailedMovieInfo = JsonSerializer.Deserialize<MovieInfo>(detailedContent);
                            movieInfos.Add(detailedMovieInfo);
                        }
                    }

                    return movieInfos;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
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
}
