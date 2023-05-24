using MediatR;
using Microsoft.EntityFrameworkCore;
using Railsense.Core.Models;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class QueryTrainDetectionsHandler : IRequestHandler<QueryTrainDetectionsRequest, IEnumerable<TrainDetection>>
{
    private readonly ApplicationDbContext _dbContext;

    public QueryTrainDetectionsHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TrainDetection>> Handle(QueryTrainDetectionsRequest request,
        CancellationToken cancellationToken)
    {
        // Builds the trainDetectionsQueryRequest
        IQueryable<TrainDetection> query = _dbContext.Traffic
            .Include(x => x.Agency)
            .Include(x => x.ServiceCategory)
            .Include(x => x.Location)
            .Include(x => x.Location.GeoLocation)
            .Include(x => x.DepartureLocation)
            .Include(x => x.DepartureLocation.GeoLocation)
            .Include(x => x.ArrivalLocation)
            .Include(x => x.ArrivalLocation.GeoLocation)
            .Include(x => x.ArrivalLocation);

        if (!request.Agency.Equals(string.Empty))
            query = query.Where(x =>
                x.Agency.Name.Equals(request.Agency) || x.Agency.Aliases.Any(y => y.Name.Equals(request.Agency)));

        if (!request.ServiceCategory.Equals(string.Empty))
            query = query.Where(x =>
                x.ServiceCategory.Name.Equals(request.ServiceCategory) ||
                x.ServiceCategory.Aliases.Any(y => y.Name.Equals(request.ServiceCategory)));

        if (!request.ServiceCategoryKind.Equals(null))
            query = query.Where(x => x.ServiceCategory.Kind.Equals(request.ServiceCategoryKind));

        if (!request.Node.Equals(string.Empty))
            query = query.Where(x =>
                x.Location.Name.Equals(request.Node) || x.Location.Aliases.Any(y => y.Name.Equals(request.Node)));

        if (!request.DepartureNode.Equals(string.Empty))
            query = query.Where(x =>
                x.DepartureLocation.Name.Equals(request.DepartureNode) ||
                x.DepartureLocation.Aliases.Any(y => y.Name.Equals(request.DepartureNode)));

        if (!request.ArrivalNode.Equals(string.Empty))
            query = query.Where(x =>
                x.ArrivalLocation.Name.Equals(request.ArrivalNode) ||
                x.ArrivalLocation.Aliases.Any(y => y.Name.Equals(request.ArrivalNode)));

        if (!request.MaxArrivalDelta.Equals(null)) query = query.Where(x => x.ArrivalDelta <= request.MaxArrivalDelta);

        if (!request.MinArrivalDelta.Equals(null)) query = query.Where(x => x.ArrivalDelta >= request.MinArrivalDelta);

        // Execute trainDetectionsQueryRequest.
        var result = await query.ToListAsync(cancellationToken);
        return result;
    }
}