using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Railsense.Cli;
using Railsense.Cli.Commands;
using Railsense.Data;
using Railsense.Geospatial.Services;
using Railsense.Mediator.RequestHandlers;
using Railsense.Neural.Models;
using Railsense.Neural.Services;
using Spectre.Cli.Extensions.DependencyInjection;
using Spectre.Console.Cli;

static ServiceCollection ConfigureServices()
{
    var services = new ServiceCollection();

    // Setup application settings.
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
        .AddJsonFile("appsettings.json", false)
        .Build();

    // Setup Entity Framework.
    services.AddDbContext<ApplicationDbContext>();
    services.AddPredictionEnginePool<DeltaPredictionInput, DeltaPredictionOutput>()
        .FromFile("ArrivalDeltaPredictor", "deltaArrivalModel.zip");
    services.AddMediatR(typeof(CreateAgencyHandler));
    services.AddSingleton(configuration);
    services.AddSingleton<IGeospatialService, GeospatialService>();
    services.AddSingleton<ITrainDetectionFeaturizer, TrainDetectionFeaturizer>();
    services.AddSingleton<IArrivalDeltaPredictorTrainer, ArrivalDeltaPredictorTrainer>();
    services.AddSingleton<IArrivalDeltaPredictor, ArrivalDeltaPredictor>();

    return services;
}

// Execute application entry point.
var registrar = new DependencyInjectionRegistrar(ConfigureServices());

var app = new CommandApp(registrar);
app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
    config.AddCommand<ExportTrafficReportCommand>("export-traffic-report");
    config.AddCommand<TrainDeltaPredictionModelCommand>("train-delta-prediction");
    config.AddCommand<PredictArrivalDeltaCommand>("predict-arrival-delta");
});

// Print header.
ConsoleUtils.PrintHeader("Command Line Interface");
return app.Run(args);