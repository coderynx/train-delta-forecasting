using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Railsense.Core.Models.Base;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class AddAliasToServiceCategoryHandler : IRequestHandler<AddAliasToServiceCategory, Unit>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AddAliasToAgencHandler> _logger;

    public AddAliasToServiceCategoryHandler(ApplicationDbContext dbContext,
        ILogger<AddAliasToAgencHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<Unit> Handle(AddAliasToServiceCategory request, CancellationToken cancellationToken)
    {
        // Gets the requested entity from the DB.
        var serviceCategory = _dbContext.ServiceCategories.Include(x => x.Aliases)
            .SingleOrDefault(x => x.Name == request.Name);

        // Guards for null.
        Guard.Against.Null(serviceCategory, nameof(serviceCategory));

        // Adds the alias and saves the changes.
        var newAlias = new Alias(request.Alias);
        serviceCategory.Aliases.Add(new Alias(request.Alias));
        _dbContext.SaveChanges();

        _logger.LogInformation("Added new Service category Alias {@Alias}", newAlias);

        // Returns the newly created entity.
        return Task.FromResult(new Unit());
    }
}