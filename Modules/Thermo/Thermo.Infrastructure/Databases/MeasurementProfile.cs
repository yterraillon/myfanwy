using Thermo.Application.Models;

namespace Thermo.Infrastructure.Databases;

public class MeasurementProfile : Profile
{
    public MeasurementProfile()
    {
        CreateMap<MeasurementDto, Measurement>().ReverseMap();
    }
}