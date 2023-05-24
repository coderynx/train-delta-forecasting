using Railsense.Core.Enums;
using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class RouteSegment : BaseEntity
{
    public RouteSegment()
    {
        GeoPoints = new List<GeoPoint>();
        Locations = new List<Location>();
    }

    public ICollection<GeoPoint> GeoPoints { get; set; }
    public ICollection<Location> Locations { get; set; }
    public Department Department { get; set; }
    public double Lenght { get; set; }
    public string LenghtNotes { get; set; }
    public int NumberOfTracks { get; set; }
    public bool IsHighSpeed { get; set; }
    public double InitialProgressive { get; set; }
    public double FinalProgressive { get; set; }
    public string? ClosureReason { get; set; }
    public AreaController AreaController { get; set; }
    public RouteTraction Traction { get; set; }
    public RouteRegime Regime { get; set; }
    public RouteSystem System { get; set; }
    public TrafficManagementSystem TrafficManagementSystem { get; set; }
    public SpeedRanks SpeedRanks { get; set; }
    public string? AdditionalNotes { get; set; }
}