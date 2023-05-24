using MediatR;
using Microsoft.EntityFrameworkCore;
using Railsense.Core.Models;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class LocationsQueryHandler : IRequestHandler<QueryLocationsRequest, List<Location>>
{
    private readonly ApplicationDbContext _dbContext;

    public LocationsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Location>> Handle(QueryLocationsRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Location> locationsQuery = _dbContext.Locations
            .Include(x => x.Aliases)
            .Include(x => x.GeoLocation);

        if (!request.Name.Equals(string.Empty) && request.ExactName.Equals(true))
            locationsQuery = locationsQuery.Where(x =>
                x.Name.Equals(request.Name) || x.Aliases.Any(y => y.Name.Equals(request.Name)));

        if (!request.Name.Equals(string.Empty) && request.ExactName.Equals(false))
            locationsQuery = locationsQuery.Where(x =>
                x.Name.Contains(request.Name) || x.Aliases.Any(y => y.Name.Contains(request.Name)));

        if (request.Department is not null)
            locationsQuery = locationsQuery.Where(x => x.Department.Equals(request.Department));

        if (request.AreaController is not null)
            locationsQuery = locationsQuery.Where(x => x.AreaController.Equals(request.AreaController));

        if (request.Kind is not null) locationsQuery = locationsQuery.Where(x => x.Kind.Equals(request.Kind));

        if (request.RegionalCapital) locationsQuery = locationsQuery.Where(x => x.IsRegionalCapital);

        if (request.ProvinceCapital) locationsQuery = locationsQuery.Where(x => x.IsProvinceCapital);

        if (request.PassengerService) locationsQuery = locationsQuery.Where(x => x.DoesPassengerService);

        if (request.CargoService) locationsQuery = locationsQuery.Where(x => x.DoesCargoService);

        if (request.BranchesOut) locationsQuery = locationsQuery.Where(x => x.DoesBranchOut);

        if (request.Critical) locationsQuery = locationsQuery.Where(x => x.IsCritical);

        if (request.ClosureReason) locationsQuery = locationsQuery.Where(x => x.ClosureReason != null);

        if (request.AdditionalNotes) locationsQuery = locationsQuery.Where(x => x.AdditionalNotes != null);

        var result = await locationsQuery.ToListAsync(cancellationToken);
        return result;
    }
}