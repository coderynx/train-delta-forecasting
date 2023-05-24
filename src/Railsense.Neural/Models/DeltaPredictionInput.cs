using Microsoft.ML.Data;

namespace Railsense.Neural.Models;

public class DeltaPredictionInput
{
    [ColumnName("DepartureMonth")] public string DepartureMonth { get; set; }

    [ColumnName("DepartureDayOfWeek")] public string DepartureDayOfWeek { get; set; }

    [ColumnName("ArrivalNumber")] public float ArrivalNumber { get; set; }

    [ColumnName("Agency")] public string Agency { get; set; }

    [ColumnName("ServiceCategory")] public string ServiceCategory { get; set; }

    [ColumnName("DepartureLocation")] public string DepartureLocation { get; set; }

    [ColumnName("DepartureLocationDepartment")]
    public string DepartureLocationDepartment { get; set; }

    [ColumnName("Location")] public string Location { get; set; }

    [ColumnName("LocationDepartment")] public string LocationDepartment { get; set; }

    [ColumnName("FractionTraveled")] public float FractionTraveled { get; set; }

    [ColumnName("ArrivalLocation")] public string ArrivalLocation { get; set; }

    [ColumnName("ArrivalLocationDepartment")]
    public string ArrivalLocationDepartment { get; set; }

    [ColumnName("ScheduledArrivalHour")] public float ScheduledArrivalHour { get; set; }

    [ColumnName("ScheduledArrivalMinute")] public float ScheduledArrivalMinute { get; set; }

    [ColumnName("ArrivalDelta")] public float ArrivalDelta { get; set; }
}