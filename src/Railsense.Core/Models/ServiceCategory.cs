using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public enum ServiceCategoryKind
{
    HighSpeed,
    Passenger,
    Cargo,
    Other
}

public class ServiceCategory : RailwayEntity
{
    public string FullName { get; set; }
    public ServiceCategoryKind Kind { get; set; }
}