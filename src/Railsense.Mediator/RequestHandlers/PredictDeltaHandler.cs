using MediatR;
using Railsense.Mediator.Requests;
using Railsense.Neural.Models;
using Railsense.Neural.Services;

namespace Railsense.Mediator.RequestHandlers;

public class PredictDeltaHandler : IRequestHandler<PredictDelta, DeltaPredictionOutput>
{
    private readonly IArrivalDeltaPredictor _arrivalDeltaPredictor;

    public PredictDeltaHandler(IArrivalDeltaPredictor arrivalDeltaPredictor)
    {
        _arrivalDeltaPredictor = arrivalDeltaPredictor;
    }

    public Task<DeltaPredictionOutput> Handle(PredictDelta request, CancellationToken cancellationToken)
    {
        var result = _arrivalDeltaPredictor.Predict(request.DeltaPredictionInput);
        return Task.FromResult(result);
    }
}