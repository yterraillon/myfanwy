namespace Thermo.Application.Models;

public class Measurement
{
    public Guid Id { get; init; }
    public double Temperature { get; set; }
    public string AirQuality { get; set; } = string.Empty;
    public double Humidity { get; set; }
    public DateTime Date { get; init; }
    public string Room { get; init; } = string.Empty;

    public static Measurement? Create(double temperature, string airQuality, double humidity, string room, DateTime measureTime)
    {
        if (Rooms.AllRooms.Contains(room))
        {
            return new Measurement
            {
                Id = Guid.NewGuid(),
                Temperature = Math.Round(temperature, 1),
                AirQuality = airQuality,
                Humidity =  Math.Round(humidity, 1),
                Date = measureTime,
                Room = room
            };
        } 
        return null;
    }
    
}