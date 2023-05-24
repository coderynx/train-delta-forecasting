using Ardalis.GuardClauses;
using MediatR;
using Railsense.Mediator.Requests;
using Railsense.Neural.Services;
using Spectre.Console;

namespace Railsense.Mediator.RequestHandlers;

public class TrainDeltaPredictionModelHandler : IRequestHandler<TrainDeltaPredictionModel>
{
    private readonly IArrivalDeltaPredictorTrainer _arrivalDeltaPredictor;
    private readonly ITrainDetectionFeaturizer _detectionFeaturizer;

    private readonly IMediator _mediator;

    public TrainDeltaPredictionModelHandler(IMediator mediator,
        IArrivalDeltaPredictorTrainer arrivalDeltaPredictor,
        ITrainDetectionFeaturizer detectionFeaturizer)
    {
        _mediator = mediator;
        _arrivalDeltaPredictor = arrivalDeltaPredictor;
        _detectionFeaturizer = detectionFeaturizer;
    }

    public Task<Unit> Handle(TrainDeltaPredictionModel request, CancellationToken cancellationToken)
    {
        // Sends the traffic report query.
        var queryResult = _mediator.Send(request.DatasetRequest, cancellationToken).Result;
        Guard.Against.NullOrEmpty(queryResult, nameof(queryResult));

        // Transforms the training data.
        AnsiConsole.WriteLine("Transforming data...");
        var featurizedInputs = _detectionFeaturizer.Featurize(queryResult);

        // Trains the model and saves it.
        AnsiConsole.WriteLine("Training model...");
        _arrivalDeltaPredictor.TrainModel(featurizedInputs);
        AnsiConsole.MarkupLine("[green]Done![/]");

        return Task.FromResult(new Unit());
    }
}