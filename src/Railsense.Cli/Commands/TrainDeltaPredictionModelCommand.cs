using MediatR;
using Railsense.Mediator.Requests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Railsense.Cli.Commands;

internal sealed class TrainDeltaPredictionModelCommand : Command<TrainDeltaPredictionModelCommand.Settings>
{
    private readonly IMediator _mediator;

    public TrainDeltaPredictionModelCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.Render(new Rule("Train delta prediction model"));

        var query = new QueryTrainDetectionsRequest()
            .WithMaxArrivalDelta(600)
            .WithMinArrivalDelta(-600)
            .Build();
        var trainingRequest = new TrainDeltaPredictionModel(query);

        // Sends training request.
        _mediator.Send(trainingRequest);

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
    }
}