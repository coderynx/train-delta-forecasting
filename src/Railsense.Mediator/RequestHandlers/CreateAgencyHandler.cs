using MediatR;
using Railsense.Core.Models;
using Railsense.Core.Models.Base;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class CreateAgencyHandler : IRequestHandler<CreateAgency, Agency>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateAgencyHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Agency> Handle(CreateAgency request, CancellationToken cancellationToken)
    {
        // Compose new agency from request.
        var agency = new Agency
        {
            Name = request.Name,
            Description = request.Description
        };
        foreach (var alias in request.Aliases) agency.Aliases.Add(new Alias(alias));

        // Inserts new entity in DB and save changes.
        agency = _dbContext.Agencies.Add(agency).Entity;
        _dbContext.SaveChanges();

        // Returns the newly created entity.
        return Task.FromResult(agency);
    }
}