using System;
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
        _apiKey = "9275f68f"; //  OMDB API key
    }

    public async Task<MovieInfo> GetMovieInfoAsync(string imdbId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?i={imdbId}&apikey={_apiKey}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movieInfo = JsonSerializer.Deserialize<MovieInfo>(content);

                return movieInfo;
            }
            else
            {
                // Handle unsuccessful response
                return null;
            }
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