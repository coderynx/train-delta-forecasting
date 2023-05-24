using GeoCoordinatePortable;
using Railsense.Core.Models;

namespace Railsense.Geospatial.Services;

public class GeospatialService : IGeospatialService
{
    public float CalculateDistance(Location firstLocation, Location secondLocation)
    {
        // Composes departure point and arrival point.
        var departurePoint = new GeoCoordinate(firstLocation.GeoLocation.Latitude, firstLocation.GeoLocation.Longitude);
        var arrivalPoint = new GeoCoordinate(secondLocation.GeoLocation.Latitude, secondLocation.GeoLocation.Longitude);

        // Calculates distance.
        var distance = departurePoint.GetDistanceTo(arrivalPoint);

        // Convert meters to kilometers and rounds it up.
        distance /= 1000;
        distance = Math.Round(distance);
        return (float)distance;
    }
}