using MediatR;
using Railsense.Mediator.Requests;
using Spectre.Console;

namespace Railsense.Mediator.RequestHandlers;

public class PopulateLocationAliasesHandler : IRequestHandler<PopulateLocationAliasesRequest>
{
    private readonly IMediator _mediator;

    public PopulateLocationAliasesHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<Unit> Handle(PopulateLocationAliasesRequest request, CancellationToken cancellationToken)
    {
        foreach (var oldName in request.Locations)
        {
            // Check if the location is already stored in DB or if it's ignored.
            var oldNameQuery = new QueryLocationsRequest()
                .WithExactName(oldName)
                .Build();

            var results = _mediator.Send(oldNameQuery, cancellationToken).Result;

            var ignoredRequest = new CheckIfLocationIsIgnoredRequest(oldName);
            var ignored = _mediator.Send(ignoredRequest, cancellationToken).Result;
            if (results.Any() || ignored) continue;

            while (true)
            {
                // Request new name prompt.
                AnsiConsole.WriteLine($"\n{oldName} was not found.");
                var newName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Type the correct name: ")
                        .DefaultValue(oldName));

                // Checks if the new node name is already stored in DB.
                var newNameQuery = new QueryLocationsRequest()
                    .WithExactName(newName)
                    .Build();

                results = _mediator.Send(newNameQuery, cancellationToken).Result;
                if (results.Any())
                {
                    var addAliasToNode = new AddAliasToLocation(newName, oldName);
                    _mediator.Send(addAliasToNode, cancellationToken);
                    break;
                }

                // Prompts to the user to ignore the location.
                AnsiConsole.MarkupLine($"The name {newName} was [red]not found[/]");
                var skip = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Do you want to ignore this location?")
                        .AddChoices("No", "Yes")).ToLower();
                if (!skip.Equals("yes") && !skip.Equals("y")) continue;

                // Save the location as ignored to DB.
                var locationIgnored = new CreateIgnoredLocation(oldName);
                _mediator.Send(locationIgnored, cancellationToken);
                break;
            }
        }

        return Task.FromResult(new Unit());
    }
}