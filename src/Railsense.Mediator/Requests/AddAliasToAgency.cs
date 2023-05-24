using MediatR;

namespace Railsense.Mediator.Requests;

public class AddAliasToAgency : IRequest<Unit>
{
    public AddAliasToAgency(string name, string alias)
    {
        Name = name;
        Alias = alias;
    }

    public string Name { get; set; }
    public string Alias { get; set; }
}