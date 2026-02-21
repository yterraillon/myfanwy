namespace Thermo.Application;

public interface IThermoReadRepository
{
    public IEnumerable<Measurement> GetAllMeasurementsBetween(int daysInterval);

    public IEnumerable<Measurement> GetAllMeasurementsBefore(DateTime date);
}