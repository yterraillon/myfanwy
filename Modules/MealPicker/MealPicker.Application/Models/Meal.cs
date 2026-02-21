namespace MealPicker.Application.Models;

public class Meal
{
    public string Name { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = [];
    public Guid Id { get; set; }
}