using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class GeoPoint : BaseEntity
{
    public GeoPoint(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}