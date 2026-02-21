using Infrastructure.Databases;
using LiteDB;

namespace Thermo.Infrastructure.Databases;

public class ThermoDbContext
{
    public LiteDatabase Database { get; }

    public ThermoDbContext(IDbContext context)
    {
        Database = context.Database;
        Database.GetCollection<MeasurementDto>()
            .EnsureIndex(x => x.Id, true);
    }
}