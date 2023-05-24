using MediatR;
using Railsense.Mediator.Requests;
using Spectre.Console;

namespace Railsense.Mediator.RequestHandlers;

public class
    PopulateServiceCategoriesHandler : IRequestHandler<
        PopulateServiceAliasesRequest>
{
    private readonly IMediator _mediator;

    public PopulateServiceCategoriesHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<Unit> Handle(PopulateServiceAliasesRequest request,
        CancellationToken cancellationToken)
    {
        foreach (var oldName in request.ServiceCategories)
        {
            // Checks if the service category is already stored in DB.
            var oldServiceCategoryRequest = new ServiceCategoriesQueryRequest()
                .WithExactName(oldName)
                .Build();

            var results = _mediator.Send(oldServiceCategoryRequest, cancellationToken).Result;
            if (results.Any()) continue;

            // Request new name prompt.
            var newName = AnsiConsole.Prompt(
                new TextPrompt<string>($"How do you want to store [green]{oldName}[/]? ")
                    .DefaultValue(oldName)
                    .ShowDefaultValue(false));

            // Checks if the new new service category name is already stored in DB.
            var newServiceCategoryRequest = new ServiceCategoriesQueryRequest()
                .WithExactName(newName)
                .Build();

            results = _mediator.Send(newServiceCategoryRequest, cancellationToken).Result;
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
                    var addAliasToNode = new AddAliasToServiceCategory(newName, oldName);
                    _mediator.Send(addAliasToNode, cancellationToken);
                    continue;
                }
            }

            // Compose create service category request.
            var createServiceCategory = newName != oldName
                ? new CreateServiceCategory(newName, aliases: new[] { oldName })
                : new CreateServiceCategory(oldName, aliases: Array.Empty<string>());

            // Send create service category request.
            _mediator.Send(createServiceCategory, cancellationToken);
        }

        return Task.FromResult(new Unit());
    }
}