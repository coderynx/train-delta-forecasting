using MediatR;

namespace Railsense.Mediator.Requests;

public class PopulateServiceAliasesRequest : IRequest<Unit>
{
    public PopulateServiceAliasesRequest(IEnumerable<string> serviceCategories)
    {
        ServiceCategories = serviceCategories;
    }

    public IEnumerable<string> ServiceCategories { get; set; }
}