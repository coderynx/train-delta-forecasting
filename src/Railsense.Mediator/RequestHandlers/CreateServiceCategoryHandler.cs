using MediatR;
using Railsense.Core.Models;
using Railsense.Core.Models.Base;
using Railsense.Data;
using Railsense.Mediator.Requests;

namespace Railsense.Mediator.RequestHandlers;

public class CreateServiceCategoryHandler : IRequestHandler<CreateServiceCategory, ServiceCategory>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateServiceCategoryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ServiceCategory> Handle(CreateServiceCategory request, CancellationToken cancellationToken)
    {
        // Compose new service category from request.
        var serviceCategory = new ServiceCategory
        {
            Name = request.Name,
            FullName = request.FullName
        };
        foreach (var alias in request.Aliases) serviceCategory.Aliases.Add(new Alias(alias));

        // Inserts new entity in DB and save changes.
        serviceCategory = _dbContext.ServiceCategories.Add(serviceCategory).Entity;
        _dbContext.SaveChanges();

        // Returns the newly created entity.
        return Task.FromResult(serviceCategory);
    }
}