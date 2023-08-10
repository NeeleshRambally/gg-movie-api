using Movie.Models;

namespace Movie;

using Microsoft.EntityFrameworkCore;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
    }

    public DbSet<MovieDto> movie_tbl { get; set; } = null!;
}