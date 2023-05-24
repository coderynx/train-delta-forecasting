using Railsense.Core.Models;
using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public interface ITrainDetectionFeaturizer
{
    IEnumerable<DeltaPredictionInput> Featurize(IEnumerable<TrainDetection> detections);
}