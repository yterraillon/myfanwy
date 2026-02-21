using Infrastructure.HttpClients;
using MediatR;

namespace CineRoulette.Function;

public static class GetRandomMovie
{
    public class Handler(CineRouletteN8nClient client) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var movie = await client.GetRandomMovie();
            var viewModel = new MovieViewModel
            {
                Title = movie.Title,
                ImageUrl = movie.Poster,
                Duration = FormatDuration(movie)
            };
            return new Response(viewModel);
        }

        private static string FormatDuration(Movie movie)
        {
            if (movie.Duration <= 0)
                return "0min";

            var hours = movie.Duration / 3600;
            var minutes = (movie.Duration % 3600) / 60;

            if (hours > 0)
                return $"{hours}h {minutes:D2}min";
        
            return $"{minutes}min";
        }
    }
    
    // ReSharper disable once InconsistentNaming
    public class CineRouletteN8nClient(HttpClient client)
    {
        public async Task<Movie> GetRandomMovie()
        {
            var movie = await client.GetRequest<Movie>("");
            return movie ?? new Movie();
        }
    }
    
    public class Request : IRequest<Response>;

    public record Response(MovieViewModel Movie);
    
    public record MovieViewModel(string Title = "", string ImageUrl = "", string Duration = "0 min");
}