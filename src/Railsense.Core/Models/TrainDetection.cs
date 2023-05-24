using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Railsense.Core.Models;

public class TrainDetection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Location Location { get; set; }

    public DateTime DepartureDate { get; set; }
    public int ArrivalNumber { get; set; }
    public int DepartureNumber { get; set; }

    [ForeignKey("Agency")] public Guid AgencyId { get; set; }

    public Agency Agency { get; set; }

    [ForeignKey("ServiceCategory")] public Guid ServiceCategoryId { get; set; }

    public ServiceCategory ServiceCategory { get; set; }

    public Location? ArrivalLocation { get; set; }
    public Location? DepartureLocation { get; set; }

    public DateTime ScheduledArrivalTime { get; set; }
    public DateTime RealArrivalTime { get; set; }
    public int RealArrivalPlatform { get; set; }
    public float ArrivalDelta { get; set; }

    public DateTime ScheduledDepartureTime { get; set; }
    public DateTime RealDepartureTime { get; set; }
    public int RealDeparturePlatform { get; set; }
    public float DepartureDelta { get; set; }

    public bool DoesStop { get; set; }
}