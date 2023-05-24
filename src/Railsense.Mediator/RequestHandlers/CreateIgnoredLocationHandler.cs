using MediatR;
using Railsense.Core.Models;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class CreateIgnoredLocationHandler : IRequestHandler<CreateIgnoredLocation, IgnoredLocation>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateIgnoredLocationHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IgnoredLocation> Handle(CreateIgnoredLocation request, CancellationToken cancellationToken)
    {
        var ignoredLocation = new IgnoredLocation(request.Name);

        ignoredLocation = _dbContext.IgnoredLocations.Add(ignoredLocation).Entity;
        _dbContext.SaveChanges();

        return Task.FromResult(ignoredLocation);
    }
}