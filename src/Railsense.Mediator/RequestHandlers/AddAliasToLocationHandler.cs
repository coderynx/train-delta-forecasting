using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Railsense.Core.Models.Base;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class AddAliasToLocationHandler : IRequestHandler<AddAliasToLocation, Unit>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddAliasToLocationHandler> _logger;

    public AddAliasToLocationHandler(ApplicationDbContext dbContext,
        ILogger<AddAliasToLocationHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<Unit> Handle(AddAliasToLocation request, CancellationToken cancellationToken)
    {
        // Inserts new node in DB and save changes.
        var location = _dbContext.Locations.Include(x => x.Aliases)
            .SingleOrDefault(x => x.Name == request.Name);

        // Guards for null.
        Guard.Against.Null(location, nameof(location));

        // Adds the new alias and saves changes.
        var newAlias = new Alias(request.Alias);
        location.Aliases.Add(new Alias(request.Alias));
        _dbContext.SaveChanges();

        _logger.LogInformation("Added new Location Alias {@Alias}", newAlias);

        return Task.FromResult(new Unit());
    }
}