using MediatR;

namespace Railsense.Mediator.Requests;

public class AddAliasToLocation : IRequest<Unit>
{
    public AddAliasToLocation(string name, string alias)
    {
        Name = name;
        Alias = alias;
    }

    public string Name { get; set; }
    public string Alias { get; set; }
}