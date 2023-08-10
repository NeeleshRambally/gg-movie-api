using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Npgsql;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MovieSimple
{
    public class Function
    {
        private static readonly string connectionString =
            "Host=database-1.c8kk38a4lxin.us-east-1.rds.amazonaws.com;Port=5432;Username=postgres;Password=nilesh084R;Database=postgres";

        private List<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand("SELECT id,year,title,genre,director,actors,rating from movie_tbl",
                connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var movie = new MovieDto
                {
                    id = reader.GetInt32(0),
                    year = reader.GetString(1),
                    title = reader.GetString(2),
                    director = reader.GetString(4),
                    rating = reader.GetString(6)
                };
                var genres = reader.GetFieldValue<List<string>>(3);
                movie.genre = genres;
                var actors = reader.GetFieldValue<List<string>>(5);
                movie.actors = actors;
                movies.Add(movie);
            }

            connection.Close();
            return movies;
        }


        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent,
            ILambdaContext context)
        {
            try
            {
                LambdaLogger.Log("Starting lambda execution");
                var movies = GetAllMovies();
                if (movies.Count == 0)
                {
                    LambdaLogger.Log("No Movies in DB");
                    return new APIGatewayProxyResponse
                    {
                        Body = "No movies in db",
                        StatusCode = 404,
                        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                    };
                }

                return new APIGatewayProxyResponse
                {
                    Body = JsonSerializer.Serialize(movies),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.StackTrace);
                return new APIGatewayProxyResponse
                {
                    Body = "ERROR while trying to fetch movies",
                    StatusCode = 500,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
        }
    }
}