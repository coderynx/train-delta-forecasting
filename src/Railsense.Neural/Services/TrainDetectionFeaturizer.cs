using System.Globalization;
using Railsense.Core.Models;
using Railsense.Geospatial.Services;
using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public class TrainDetectionFeaturizer : ITrainDetectionFeaturizer
{
    private readonly IGeospatialService _geospatialService;

    public TrainDetectionFeaturizer(IGeospatialService geospatialService)
    {
        _geospatialService = geospatialService;
    }

    public IEnumerable<DeltaPredictionInput> Featurize(IEnumerable<TrainDetection> detections)
    {
        var inputs = new List<DeltaPredictionInput>();

        foreach (var detection in detections)
        {
            // Calculates the distance between the locations.
            var departureAndArrivalLocationDistance =
                _geospatialService.CalculateDistance(detection.DepartureLocation, detection.ArrivalLocation);
            var departureAndCurrentLocationDistance =
                _geospatialService.CalculateDistance(detection.DepartureLocation, detection.Location);

            var input = new DeltaPredictionInput
            {
                DepartureMonth = detection.DepartureDate.ToString("MMMM", CultureInfo.InvariantCulture),
                DepartureDayOfWeek = detection.DepartureDate.DayOfWeek.ToString(),
                ArrivalNumber = detection.ArrivalNumber.Equals(0) ? detection.DepartureNumber : detection.ArrivalNumber,
                Agency = detection.Agency.Name,
                ServiceCategory = detection.ServiceCategory.Name,
                DepartureLocation = detection.DepartureLocation.Name,
                DepartureLocationDepartment = detection.DepartureLocation.Department.ToString(),
                Location = detection.Location.Name,
                LocationDepartment = detection.Location.Department.ToString(),
                FractionTraveled = departureAndArrivalLocationDistance / departureAndCurrentLocationDistance,
                ArrivalLocation = detection.ArrivalLocation.Name,
                ArrivalLocationDepartment = detection.ArrivalLocation.Department.ToString(),
                ScheduledArrivalHour = detection.ScheduledArrivalTime.Hour,
                ScheduledArrivalMinute = detection.ScheduledArrivalTime.Minute,
                ArrivalDelta = detection.ArrivalDelta
            };

            inputs.Add(input);
        }

        return inputs;
    }
}