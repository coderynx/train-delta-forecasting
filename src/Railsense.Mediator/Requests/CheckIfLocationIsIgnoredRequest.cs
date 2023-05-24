using MediatR;

namespace Railsense.Mediator.Requests;

public class CheckIfLocationIsIgnoredRequest : IRequest<bool>
{
    public CheckIfLocationIsIgnoredRequest(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}