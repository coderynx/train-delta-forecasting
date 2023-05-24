namespace Railsense.Mediator.Requests;

public interface ICreateRailwayEntityRequest
{
    string Name { get; set; }
    string[] Aliases { get; set; }
}