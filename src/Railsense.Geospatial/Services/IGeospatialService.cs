using Railsense.Core.Models;

namespace Railsense.Geospatial.Services;

public interface IGeospatialService
{
    float CalculateDistance(Location firstLocation, Location secondLocation);
}