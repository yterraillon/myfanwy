using System.Globalization;

namespace Thermo.Application.Features;

public static class GetAllMeasurements
{
    public class Handler(IThermoReadRepository measurementRepository) : IRequestHandler<Request,MeasurementsViewModel>
    {
        public Task<MeasurementsViewModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var measurements = measurementRepository.GetAllMeasurementsBetween(request.DaysInterval)
                .OrderBy(m => m.Date)
                .ToList();
            
            var viewModel = ConvertToMeasurementViewModel(measurements);
            return Task.FromResult(viewModel);
        }
        
        private static MeasurementsViewModel ConvertToMeasurementViewModel(List<Measurement> measurements)
        {
            var viewModel = new MeasurementsViewModel();
            foreach (var measurement in measurements)
            {
                viewModel.AddMeasurement(measurement);
            }
            
            viewModel.LatestMeasurementInRoom = viewModel.GetLatestMeasurementInRoom(Rooms.Room1);
            viewModel.LatestMeasurementInRoom2 = viewModel.GetLatestMeasurementInRoom(Rooms.Room2);
            viewModel.LatestMeasurementInLivingRoom = viewModel.GetLatestMeasurementInRoom(Rooms.LivingRoom);
            viewModel.LatestMeasurementOutside = viewModel.GetLatestMeasurementInRoom(Rooms.Outside);

            // viewModel.Data = viewModel.Data.OrderBy(m => m.Date).ToList();
            
            var labels = CreateLabels(measurements);
            viewModel.SetLabels(labels);
            
            
            return viewModel;
        }

        private static List<string> CreateLabels(List<Measurement> measurements) =>
            measurements.OrderBy(m => m.Date)
                .Select(m => m.Date.ToString(new CultureInfo("fr-FR")))
                .Distinct()
                .ToList();
    }
    
    public record Request(int DaysInterval = 1) : IRequest<MeasurementsViewModel>;

    public class MeasurementsViewModel
    {
        public List<TemperatureMeasurement> Temperatures { get; } = [];
        public List<HumidityMeasurement> Humidity { get; } = [];
        public List<AirQualityMeasurement> AirQuality { get; } = [];
        public List<string> Labels { get; private set; } = [];

        public List<MeasurementViewModel> Data { get; set; } = [];
        
        public MeasurementViewModel? LatestMeasurementInRoom { get; set; }
        public MeasurementViewModel? LatestMeasurementInRoom2 { get; set; }
        public MeasurementViewModel? LatestMeasurementInLivingRoom { get; set; }
        public MeasurementViewModel? LatestMeasurementOutside { get; set; }
        
        public void AddMeasurement(Measurement measurement)
        {
            Temperatures.Add(new TemperatureMeasurement(measurement.Id, measurement.Temperature, measurement.Room, measurement.Date));
            Humidity.Add(new HumidityMeasurement(measurement.Id, measurement.Humidity, measurement.Room, measurement.Date));
            AirQuality.Add(new AirQualityMeasurement(measurement.Id, ConvertAirQualityToAirQualityIndex(measurement.AirQuality), measurement.Room, measurement.Date));
            Data.Add(new MeasurementViewModel(measurement.Id, measurement.Temperature, measurement.Humidity,measurement.AirQuality, measurement.Room, measurement.Date));
        }

        public MeasurementViewModel? GetLatestMeasurementInRoom(string room) =>
            Data.Where(m => m.Room == room)
                .OrderByDescending(m => m.Date)
                .FirstOrDefault();
        
        public void SetLabels(List<string> labels) => Labels = labels;

        private static double ConvertAirQualityToAirQualityIndex(string airQuality) =>
            airQuality switch
            {
                "Très Mauvaise" => 1,
                "Mauvaise" => 2,
                "Moyenne" => 3,
                "Bonne" => 4,
                "Excellente" => 5,
                _ => 0 //throw new ArgumentOutOfRangeException(nameof(airQuality), airQuality, null)
            };
    }
    
    public record TemperatureMeasurement (Guid Id, double Temperature, string Room, DateTime Date);
    public record HumidityMeasurement (Guid Id, double Humidity, string Room, DateTime Date);
    public record AirQualityMeasurement (Guid Id, double AirQuality, string Room, DateTime Date);
    public record MeasurementViewModel(Guid Id, double Temperature, double Humidity, string AirQuality, string Room, DateTime Date);
}