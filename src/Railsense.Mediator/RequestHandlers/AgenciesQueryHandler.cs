using MediatR;
using Microsoft.EntityFrameworkCore;
using Railsense.Core.Models;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class AgenciesQueryHandler : IRequestHandler<QueryAgenciesRequest, List<Agency>>
{
    private readonly ApplicationDbContext _dbContext;

    public AgenciesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Agency>> Handle(QueryAgenciesRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Agency> query = _dbContext.Agencies
            .Include(x => x.Aliases);

        if (!request.Name.Equals(string.Empty) && request.ExactName)
            query = query.Where(x => x.Name.Equals(request.Name) || x.Aliases.Any(y => y.Name.Equals(request.Name)));

        if (!request.Name.Equals(string.Empty) && !request.ExactName)
            query = query.Where(x =>
                x.Name.Contains(request.Name) || x.Aliases.Any(y => y.Name.Contains(request.Name)));

        // Executes the query.
        var result = query.ToList();
        return Task.FromResult(result);
    }
}