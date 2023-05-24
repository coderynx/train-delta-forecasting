using MediatR;
using Railsense.Neural.Models;

namespace Railsense.Mediator.Requests;

public class PredictDelta : IRequest<DeltaPredictionOutput>
{
    public PredictDelta(DeltaPredictionInput deltaPredictionInput)
    {
        DeltaPredictionInput = deltaPredictionInput;
    }

    public DeltaPredictionInput DeltaPredictionInput { get; set; }
}