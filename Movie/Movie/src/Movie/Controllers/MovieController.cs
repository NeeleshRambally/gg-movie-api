using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.Mvc;
using Movie.Services;


namespace Movie.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _service;

    public MovieController(IMovieService service)
    {
        _service = service;
    }
    
    [HttpGet("get/")]
    public async Task<APIGatewayProxyResponse> GetAllMovies()
    {
        try
        {
            var movielist = _service.GetAllMovies();
            if (movielist.Count == 0)
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = 404,
                    Body = "No Movies Found",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
                return response;
            }
            else
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonSerializer.Serialize(movielist),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
                return response;
            }
        }
        catch (Exception ex)
        {
            return  GetErrorResponse(ex.Message);;
        }
    }

    [HttpGet("get/{title}")]
    public async Task<APIGatewayProxyResponse> GetMovieByTitle(string title)
    {
        try
        {
            var movie = _service.GetMovieByTitle(title);
            if (movie == null)
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = 404,
                    Body = "Movie Not Found",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
                return response;
            }
            else
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonSerializer.Serialize(movie),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
                return response;
            }
        }
        catch (Exception ex)
        {
            return GetErrorResponse(ex.Message);
        }
    }

    private static APIGatewayProxyResponse GetErrorResponse(string message)
    {
        var response = new APIGatewayProxyResponse
        {
            StatusCode = 500,
            Body = $"An error occurred: {message}",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
        return response;
    }
}