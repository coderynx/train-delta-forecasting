using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public class CreateServiceCategory : IRequest<ServiceCategory>, ICreateRailwayEntityRequest
{
    public CreateServiceCategory(string name, string fullName = "", string[] aliases = null!)
    {
        Name = name;
        FullName = fullName;
        Aliases = aliases;
    }

    public string FullName { get; set; }

    public string Name { get; set; }
    public string[] Aliases { get; set; }
}