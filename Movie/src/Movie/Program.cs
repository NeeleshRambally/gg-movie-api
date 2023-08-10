using Microsoft.EntityFrameworkCore;
using Movie;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieContext>(opt =>
    opt.UseNpgsql("Host=database-1.c8kk38a4lxin.us-east-1.rds.amazonaws.com;Port=5432;Username=postgres;Password=nilesh084R;Database=postgres"));

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddScoped<IMovieService,MovieService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", () => "Movie API - Games Global - Running!");

app.Run();