using System.ComponentModel.DataAnnotations.Schema;
using Railsense.Core.Enums;
using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class Location : BaseEntity
{
    public Location()
    {
        Aliases = new List<Alias>();
        TrainDetections = new List<TrainDetection>();
        ArrivalTrainDetections = new List<TrainDetection>();
        DepartureTrainDetections = new List<TrainDetection>();
    }

    public int Code { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public ICollection<RouteSegment> RouteSegments { get; set; }
    public ICollection<Alias> Aliases { get; set; }
    public GeoPoint GeoLocation { get; set; }
    public Department Department { get; set; }
    public LocationKind Kind { get; set; }
    public bool DoesPassengerService { get; set; }
    public bool DoesCargoService { get; set; }
    public bool DoesBranchOut { get; set; }
    public bool IsCritical { get; set; }
    public bool IsRegionalCapital { get; set; }
    public bool IsProvinceCapital { get; set; }
    public string? ClosureReason { get; set; }
    public AreaController AreaController { get; set; }

    public string? AdditionalNotes { get; set; }

    public ICollection<TrainDetection> TrainDetections { get; set; }

    [InverseProperty("ArrivalLocation")] public ICollection<TrainDetection> ArrivalTrainDetections { get; set; }

    [InverseProperty("DepartureLocation")] public ICollection<TrainDetection> DepartureTrainDetections { get; set; }
}