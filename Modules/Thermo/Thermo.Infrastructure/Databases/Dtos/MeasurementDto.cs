namespace Thermo.Infrastructure.Databases.Dtos;

public class MeasurementDto
{
    public Guid Id { get; init; }
    public double Temperature { get; init; }
    public string AirQuality { get; init; } = string.Empty;
    public double Humidity { get; init; }
    public DateTime Date { get; init; }
    public string Room { get; init; } = string.Empty;
}