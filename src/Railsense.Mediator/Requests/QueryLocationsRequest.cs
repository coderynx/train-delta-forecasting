using MediatR;
using Railsense.Core.Enums;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public interface ILocationsQuery
{
    ILocationsQuery WithName(string name);
    ILocationsQuery OfDepartment(Department department);
    ILocationsQuery OfKind(LocationKind kind);
    ILocationsQuery DoesPassengerService();
    ILocationsQuery DoesCargoService();
    ILocationsQuery DoesBranchOut();
    ILocationsQuery IsCritical();
    ILocationsQuery IsRegionalCapital();
    ILocationsQuery IsProvinceCapital();
    ILocationsQuery HasClosureReason();
    ILocationsQuery OfAreaController(AreaController areaController);
    ILocationsQuery HasAdditionalNotes();
    QueryLocationsRequest Build();
}

public class QueryLocationsRequest : IRequest<List<Location>>, ILocationsQuery
{
    public string? Name { get; set; }
    public bool ExactName { get; set; }
    public Department? Department { get; set; }
    public LocationKind? Kind { get; set; }
    public bool PassengerService { get; set; }
    public bool CargoService { get; set; }
    public bool BranchesOut { get; set; }
    public bool Critical { get; set; }
    public bool RegionalCapital { get; set; }
    public bool ProvinceCapital { get; set; }
    public bool ClosureReason { get; set; }
    public AreaController? AreaController { get; set; }
    public bool AdditionalNotes { get; set; }

    public ILocationsQuery WithName(string? name)
    {
        Name = name;
        ExactName = false;
        return this;
    }

    public ILocationsQuery OfDepartment(Department department)
    {
        Department = department;
        return this;
    }

    public ILocationsQuery OfKind(LocationKind kind)
    {
        Kind = kind;
        return this;
    }

    public ILocationsQuery DoesPassengerService()
    {
        PassengerService = true;
        return this;
    }

    public ILocationsQuery DoesCargoService()
    {
        CargoService = true;
        return this;
    }

    public ILocationsQuery DoesBranchOut()
    {
        BranchesOut = true;
        return this;
    }

    public ILocationsQuery IsCritical()
    {
        Critical = true;
        return this;
    }

    public ILocationsQuery IsRegionalCapital()
    {
        RegionalCapital = true;
        return this;
    }

    public ILocationsQuery IsProvinceCapital()
    {
        ProvinceCapital = true;
        return this;
    }

    public ILocationsQuery HasClosureReason()
    {
        ClosureReason = true;
        return this;
    }

    public ILocationsQuery OfAreaController(AreaController areaController)
    {
        AreaController = areaController;
        return this;
    }

    public ILocationsQuery HasAdditionalNotes()
    {
        AdditionalNotes = true;
        return this;
    }

    public QueryLocationsRequest Build()
    {
        return this;
    }

    public ILocationsQuery WithExactName(string? name)
    {
        Name = name;
        ExactName = true;
        return this;
    }
}