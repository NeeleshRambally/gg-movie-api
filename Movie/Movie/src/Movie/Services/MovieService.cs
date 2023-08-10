using Movie.Models;


namespace Movie.Services;

public class MovieService : IMovieService
{
    private readonly MovieContext _context;
    private readonly ILogger<MovieService> _logger;


    public MovieService(ILogger<MovieService> logger, MovieContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<MovieDto> GetAllMovies()
    {
        try
        {
            _logger.LogInformation("Getting movie list.");
            var movies = _context.movie_tbl.ToList();
            return movies;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    public MovieDto GetMovieByTitle(string title)
    {
        try
        {
            _logger.LogInformation("Searching for movie {}", title);
            var movie = _context.movie_tbl.Where(movie => movie.title.Equals(title)).ToList();
            if (movie.Any())
            {
                return movie.First();
            }

            _logger.LogWarning("No Movies Found for title {}", title);
            return null!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}