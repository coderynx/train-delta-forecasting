using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public interface IServiceCategoryQueryRequest
{
    IServiceCategoryQueryRequest WithName(string name);
    IServiceCategoryQueryRequest WithExactName(string name);
    ServiceCategoriesQueryRequest Build();
}

public class ServiceCategoriesQueryRequest : IRequest<List<ServiceCategory>>, IServiceCategoryQueryRequest
{
    public string Name { get; set; }
    public bool ExactName { get; set; }

    public IServiceCategoryQueryRequest WithName(string name)
    {
        Name = name;
        ExactName = false;
        return this;
    }

    public IServiceCategoryQueryRequest WithExactName(string name)
    {
        Name = name;
        ExactName = true;
        return this;
    }

    public ServiceCategoriesQueryRequest Build()
    {
        return this;
    }
}