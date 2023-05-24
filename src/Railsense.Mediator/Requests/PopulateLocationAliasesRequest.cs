using MediatR;

namespace Railsense.Mediator.Requests;

public class PopulateLocationAliasesRequest : IRequest<Unit>
{
    public PopulateLocationAliasesRequest(IEnumerable<string> locations)
    {
        Locations = locations;
    }

    public IEnumerable<string> Locations { get; set; }
}