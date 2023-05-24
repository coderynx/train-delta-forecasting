using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public interface IAgenciesQueryRequest
{
    IAgenciesQueryRequest WithName(string name);
    IAgenciesQueryRequest WithExactName(string name);
    QueryAgenciesRequest Build();
}

public class QueryAgenciesRequest : IRequest<List<Agency>>, IAgenciesQueryRequest
{
    public string Name { get; set; }
    public bool ExactName { get; set; }

    public IAgenciesQueryRequest WithName(string name)
    {
        Name = name;
        ExactName = false;
        return this;
    }

    public IAgenciesQueryRequest WithExactName(string name)
    {
        Name = name;
        ExactName = true;
        return this;
    }

    public QueryAgenciesRequest Build()
    {
        return this;
    }
}