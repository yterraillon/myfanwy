using Infrastructure.Databases;
using Thermo.Application;
using Thermo.Application.Models;

namespace Thermo.Infrastructure.Databases;

public class ThermoReadRepository(IDbContext context, IMapper mapper) : Repository<Measurement, MeasurementDto>(context, mapper), IThermoReadRepository
{
    private readonly IMapper _mapper = mapper;
    
    public IEnumerable<Measurement> GetAllMeasurementsBetween(int daysInterval = 1)
    {
        var daysToAdd = -daysInterval;
        
        var collection = Collection.Query()
            .Where(dto => dto.Date >= DateTime.Now.AddDays(daysToAdd))
            .ToList();
        
        return collection.Count != 0 ? _mapper.Map<IEnumerable<Measurement>>(collection) : new List<Measurement>();
    }
    
    public IEnumerable<Measurement> GetAllMeasurementsBefore(DateTime date)
    {
        var collection = Collection.Query()
            .Where(dto => dto.Date <= date)
            .ToList();
        
        return collection.Count != 0 ? _mapper.Map<IEnumerable<Measurement>>(collection) : new List<Measurement>();
    }
}