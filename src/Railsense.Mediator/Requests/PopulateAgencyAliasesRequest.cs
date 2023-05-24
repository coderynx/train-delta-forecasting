using MediatR;

namespace Railsense.Mediator.Requests;

public class PopulateAgencyAliasesRequest : IRequest<Unit>
{
    public PopulateAgencyAliasesRequest(IEnumerable<string> agencies)
    {
        Agencies = agencies;
    }

    public IEnumerable<string> Agencies { get; set; }
}