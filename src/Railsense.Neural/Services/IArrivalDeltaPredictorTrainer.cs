using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public interface IArrivalDeltaPredictorTrainer
{
    void TrainModel(IEnumerable<DeltaPredictionInput> trainData);
}