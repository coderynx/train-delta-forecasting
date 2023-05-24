using Microsoft.Extensions.ML;
using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public class ArrivalDeltaPredictor : IArrivalDeltaPredictor
{
    private readonly PredictionEnginePool<DeltaPredictionInput, DeltaPredictionOutput> _predictionEnginePool;

    public ArrivalDeltaPredictor(PredictionEnginePool<DeltaPredictionInput, DeltaPredictionOutput> predictionEnginePool)
    {
        _predictionEnginePool = predictionEnginePool;
    }

    public DeltaPredictionOutput Predict(DeltaPredictionInput input)
    {
        var result = _predictionEnginePool.Predict("ArrivalDeltaPredictor", input);
        return result;
    }
}