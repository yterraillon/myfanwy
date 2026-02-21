using Application.ObjectStorage;

namespace MealPicker.Application.Features;

public static class GetMeals
{
    public class Handler(IObjectStorageReader<List<Meal>> githubCdnClient) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            const string mealSelectionFileName = "meals.json";
            var mealSelection = await githubCdnClient.GetObjectContentAsync(mealSelectionFileName);
            
            return new Response(new MealSelection 
            {
                Meals = mealSelection
            });
        }
    }

    public record Request() : IRequest<Response>;
    
    public record Response(MealSelection MealSelection);
}