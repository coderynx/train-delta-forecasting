using MediatR;
using Railsense.Mediator.Requests;
using Spectre.Console;

namespace Railsense.Mediator.RequestHandlers;

public class PopulateAgenciesAliasesHandler : IRequestHandler<PopulateAgencyAliasesRequest>
{
    private readonly IMediator _mediator;

    public PopulateAgenciesAliasesHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<Unit> Handle(PopulateAgencyAliasesRequest request, CancellationToken cancellationToken)
    {
        foreach (var oldName in request.Agencies)
        {
            // Check if the node is already stored in DB.
            var oldAgencyQueryRequest = new QueryAgenciesRequest()
                .WithExactName(oldName)
                .Build();

            var results = _mediator.Send(oldAgencyQueryRequest, cancellationToken).Result;
            if (results.Any()) continue;

            // Request new name prompt.
            var newName = AnsiConsole.Prompt(
                new TextPrompt<string>($"How do you want to store [green]{oldName}[/]? ")
                    .DefaultValue(oldName)
                    .ShowDefaultValue(false));

            // Checks if the new agency name is already stored in DB.
            var newAgenciesQueryRequest = new QueryAgenciesRequest()
                .WithExactName(newName)
                .Build();

            results = _mediator.Send(newAgenciesQueryRequest, cancellationToken).Result;
            if (results.Any())
            {
                var saveAsAlias = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(
                            $"[green]{newName}[/] already exists, do you want to save its original name {oldName} as a new alias?")
                        .AddChoices("Yes", "No"));

                // If yes the name is going to be stored as an alias.
                if (saveAsAlias == "Yes")
                {
                    var addAliasToNode = new AddAliasToAgency(newName, oldName);
                    _mediator.Send(addAliasToNode, cancellationToken);
                    continue;
                }
            }

            // Compose create agency request.
            var createAgency = newName != oldName
                ? new CreateAgency(newName, aliases: new[] { oldName })
                : new CreateAgency(oldName, aliases: Array.Empty<string>());

            // Send create agency request.
            _mediator.Send(createAgency, cancellationToken);
        }

        return Task.FromResult(new Unit());
    }
}