using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public class CreateAgency : IRequest<Agency>, ICreateRailwayEntityRequest
{
    public CreateAgency(string name, string description = "", string[] aliases = null!)
    {
        Name = name;
        Aliases = aliases;
        Description = description;
    }

    public string Description { get; set; }

    public string Name { get; set; }
    public string[] Aliases { get; set; }
}