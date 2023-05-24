using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using Railsense.Neural.Models;

namespace Railsense.Neural.Services;

public class ArrivalDeltaPredictorTrainer : IArrivalDeltaPredictorTrainer
{
    public void TrainModel(IEnumerable<DeltaPredictionInput> trainData)
    {
        // Loads training data and converts it to DataView.
        var mlContext = new MLContext();
        var data = mlContext.Data.LoadFromEnumerable(trainData);

        // Shuffles data in dataset.
        data = mlContext.Data.ShuffleRows(data, 343);

        // Builds the pipeline and trains the model.
        var pipeline = BuildPipeline(mlContext);
        var model = pipeline.Fit(data);

        // Saves the model.
        mlContext.Model.Save(model, data.Schema, "deltaArrivalModel.zip");
    }

    private IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
    {
        // Data process configuration with pipeline data transformations
        var pipeline =

            // Performs One Hot Encoding of categorical features.
            mlContext.Transforms.Categorical.OneHotEncoding(new[]
                {
                    new InputOutputColumnPair("DepartureMonth", "DepartureMonth"),
                    new InputOutputColumnPair("DepartureDayOfWeek", "DepartureDayOfWeek"),
                    new InputOutputColumnPair("Agency", "Agency"),
                    new InputOutputColumnPair("ServiceCategory", "ServiceCategory"),
                    new InputOutputColumnPair("DepartureLocationDepartment", "DepartureLocationDepartment"),
                    new InputOutputColumnPair("LocationDepartment", "LocationDepartment"),
                    new InputOutputColumnPair("ArrivalLocationDepartment", "ArrivalLocationDepartment")
                })

                // Featurizes location names.
                .Append(mlContext.Transforms.Text.FeaturizeText("DepartureLocation", "DepartureLocation"))
                .Append(mlContext.Transforms.Text.FeaturizeText("Location", "Location"))
                .Append(mlContext.Transforms.Text.FeaturizeText("ArrivalLocation", "ArrivalLocation"))

                // Concatenates all features.
                .Append(mlContext.Transforms.Concatenate(
                    "Features", "DepartureMonth", "DepartureDayOfWeek", "ArrivalNumber", "Agency",
                    "ServiceCategory", "ScheduledArrivalHour", "ScheduledArrivalMinute", "DepartureLocation",
                    "DepartureLocationDepartment",
                    "Location", "LocationDepartment", "FractionTraveled", "ArrivalLocation",
                    "ArrivalLocationDepartment"))

                // Normalize features.
                .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))

                // Setup cache checkpoint.
                .AppendCacheCheckpoint(mlContext)

                // Appends the trainer.
                .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options
                {
                    NumberOfIterations = 150,
                    LearningRate = 0.1819408f,
                    NumberOfLeaves = 8000,
                    MinimumExampleCountPerLeaf = 10,
                    UseCategoricalSplit = true,
                    HandleMissingValue = true,
                    MinimumExampleCountPerGroup = 200,
                    MaximumCategoricalSplitPointCount = 16,
                    CategoricalSmoothing = 1,
                    L2CategoricalRegularization = 1,
                    Booster = new GradientBooster.Options
                    {
                        L2Regularization = 1,
                        L1Regularization = 0
                    },
                    BatchSize = 64,
                    LabelColumnName = "ArrivalDelta",
                    FeatureColumnName = "Features"
                }));

        return pipeline;
    }
}