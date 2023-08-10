using Movie.Models;

namespace Movie.Services;

public interface IMovieService
{
    List<MovieDto> GetAllMovies();
    
    //Assuming each title is unique
    MovieDto GetMovieByTitle(string title);
}