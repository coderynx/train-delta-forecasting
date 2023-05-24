using CsvHelper.Configuration;
using Railsense.Core.Models;

namespace Railsense.Data.Mappings;

public sealed class TrafficReportMapping : ClassMap<TrainDetection>
{
    public TrafficReportMapping()
    {
        Map(m => m.Location.Name).Index(1).Name("location");
        Map(m => m.DepartureDate).Index(2).Name("departureDate")
            .Convert(x => x.Value.DepartureDate.ToString("yyyy-mm-dd"));
        Map(m => m.ArrivalNumber).Index(3).Name("arrivalNumber");
        Map(m => m.DepartureNumber).Index(4).Name("departureNumber");
        Map(m => m.Agency.Name).Index(5).Name("agency");
        Map(m => m.ServiceCategory.Name).Index(6).Name("serviceCategory");
        Map(m => m.ArrivalLocation.Name).Index(7).Name("arrivalLocation");
        Map(m => m.DepartureLocation.Name).Index(8).Name("departureLocation");
        Map(m => m.ScheduledArrivalTime).Index(9).Name("scheduledArrivalTime")
            .Convert(x => x.Value.ScheduledArrivalTime.ToString("HH:mm:ss"));
        Map(m => m.RealArrivalTime).Index(10).Name("realArrivalTime")
            .Convert(x => x.Value.RealArrivalTime.ToString("HH:mm:ss"));
        Map(m => m.RealArrivalPlatform).Index(11).Name("realArrivalPlatform");
        Map(m => m.ArrivalDelta).Index(12).Name("arrivalDelta");
        Map(m => m.ScheduledDepartureTime).Index(13).Name("scheduledDepartureTime")
            .Convert(x => x.Value.ScheduledDepartureTime.ToString("HH:mm:ss"));
        Map(m => m.RealDepartureTime).Index(14).Name("realDepartureTime")
            .Convert(x => x.Value.RealDepartureTime.ToString("HH:mm:ss"));
        Map(m => m.RealDeparturePlatform).Index(14).Name("realDeparturePlatform");
        Map(m => m.DepartureDelta).Index(14).Name("departureDelta");
        Map(m => m.DoesStop).Index(14).Name("doesStops");
    }
}