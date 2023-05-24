using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public class CreateIgnoredLocation : IRequest<IgnoredLocation>
{
    public CreateIgnoredLocation(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}