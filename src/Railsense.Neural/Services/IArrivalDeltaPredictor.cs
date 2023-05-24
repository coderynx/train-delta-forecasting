using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public interface IArrivalDeltaPredictor
{
    DeltaPredictionOutput Predict(DeltaPredictionInput input);
}