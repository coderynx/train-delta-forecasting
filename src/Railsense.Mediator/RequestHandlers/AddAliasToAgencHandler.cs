using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Railsense.Core.Models.Base;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class AddAliasToAgencHandler : IRequestHandler<AddAliasToAgency, Unit>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddAliasToAgencHandler> _logger;

    public AddAliasToAgencHandler(ApplicationDbContext dbContext,
        ILogger<AddAliasToAgencHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddAliasToAgency request, CancellationToken cancellationToken)
    {
        // Inserts new node in DB and save changes.
        var agency = await _dbContext.Agencies.Include(x => x.Aliases)
            .SingleOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        // Guards for null.
        Guard.Against.Null(agency, nameof(agency));

        // Adds the alias and saves changes.
        var newAlias = new Alias(request.Alias);
        agency.Aliases.Add(new Alias(request.Alias));
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Added new Agency Alias {@Alias}", newAlias);

        // Returns the newly created entity.
        return new Unit();
    }
}