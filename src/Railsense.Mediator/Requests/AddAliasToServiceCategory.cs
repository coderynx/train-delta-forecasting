using MediatR;

namespace Railsense.Mediator.Requests;

public class AddAliasToServiceCategory : IRequest<Unit>
{
    public AddAliasToServiceCategory(string name, string alias)
    {
        Name = name;
        Alias = alias;
    }

    public string Name { get; set; }
    public string Alias { get; set; }
}