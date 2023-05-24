using System.Globalization;
using Ardalis.GuardClauses;
using MediatR;
using Railsense.Geospatial.Services;
using Railsense.Mediator.Requests;
using Railsense.Neural.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Railsense.Cli.Commands;

internal sealed class PredictArrivalDeltaCommand : Command<PredictArrivalDeltaCommand.Settings>
{
    private readonly IGeospatialService _geospatialService;

    private readonly IMediator _mediator;

    public PredictArrivalDeltaCommand(IMediator mediator, IGeospatialService geospatialService)
    {
        _mediator = mediator;
        _geospatialService = geospatialService;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        // TODO: Refactor this command.
        AnsiConsole.Render(new Rule("Arrival Δ prediction"));

        // Composes departure location query request.
        var departureLocationRequest = new QueryLocationsRequest()
            .WithExactName(settings.DepartureLocation)
            .Build();

        // Composes location query request.
        var locationRequest = new QueryLocationsRequest()
            .WithExactName(settings.Location)
            .Build();

        var arrivalLocationRequest = new QueryLocationsRequest()
            .WithExactName(settings.ArrivalLocation)
            .Build();

        // Sends query requests.
        var departureLocation = _mediator.Send(departureLocationRequest).Result.FirstOrDefault();
        var location = _mediator.Send(locationRequest).Result.FirstOrDefault();
        var arrivalLocation = _mediator.Send(arrivalLocationRequest).Result.FirstOrDefault();

        // Guards against nulls.
        Guard.Against.Null(departureLocation, nameof(departureLocation));
        Guard.Against.Null(location, nameof(location));
        Guard.Against.Null(arrivalLocation, nameof(arrivalLocation));

        // Calculates distances between locations.
        var distanceFromDepartureToArrivalLocation =
            _geospatialService.CalculateDistance(departureLocation, arrivalLocation);
        var distanceFromDepartureToCurrentLocation = _geospatialService.CalculateDistance(departureLocation, location);

        // Composes the model input.
        var modelInput = new DeltaPredictionInput
        {
            DepartureMonth = settings.DepartureDate.ToString("MMMM", CultureInfo.InvariantCulture),
            DepartureDayOfWeek = settings.DepartureDate.DayOfWeek.ToString(),
            ArrivalNumber = settings.ArrivalNumber,
            Agency = settings.Agency,
            ServiceCategory = settings.ServiceCategory,
            DepartureLocation = departureLocation.Name,
            DepartureLocationDepartment = departureLocation.Department.ToString(),
            Location = location.Name,
            LocationDepartment = location.Department.ToString(),
            FractionTraveled = distanceFromDepartureToArrivalLocation / distanceFromDepartureToCurrentLocation,
            ArrivalLocation = arrivalLocation.Name,
            ArrivalLocationDepartment = arrivalLocation.Department.ToString(),
            ScheduledArrivalHour = settings.ScheduledArrivalTime.Hour,
            ScheduledArrivalMinute = settings.ScheduledArrivalTime.Minute
        };

        var predictRequest = new PredictDelta(modelInput);
        var result = _mediator.Send(predictRequest).Result;
        var predictedDelta = Math.Round(result.Score, 1);

        var tree = new Tree("Train prediction request");
        tree.AddNode($":calendar: Departure date: {settings.DepartureDate:yyyy-MM-dd}");
        tree.AddNode($":: Arrival number: {settings.ArrivalNumber}");
        tree.AddNode($":: Agency: {settings.Agency}");
        tree.AddNode($":a_button_blood_type: Service category: {settings.ServiceCategory}");
        tree.AddNode($":station: Departure location: {departureLocation.Name}");
        tree.AddNode($":cityscape: Departure location department: {departureLocation.Department}");
        tree.AddNode($":station: Location: {location.Name}");
        tree.AddNode($":cityscape: Location department: {location.Department}");
        tree.AddNode($":station: Arrival location: {arrivalLocation.Name}");
        tree.AddNode($":cityscape: Arrival location department: {arrivalLocation.Department}");
        tree.AddNode($":timer_clock: Scheduled arrival time: {settings.ScheduledArrivalTime:HH:mm:ss}");
        tree.AddNode($"Distance from departure node: {distanceFromDepartureToCurrentLocation} kms");
        AnsiConsole.Render(tree);

        AnsiConsole.MarkupLine($":world_map: Predicted arrival DELTA: [purple]{predictedDelta}[/]");

        Console.ReadLine();
        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("--departure-date <DEPARTURE-DATE>")]
        public DateTime DepartureDate { get; set; }

        [CommandOption("--arrival-number <ARRIVAL-NUMBER>")]
        public float ArrivalNumber { get; set; }

        [CommandOption("--agency <AGENCY>")] public string Agency { get; set; }

        [CommandOption("--service-category <SERVICE-CATEGORY>")]
        public string ServiceCategory { get; set; }

        [CommandOption("--departure-location <DEPARTURE-LOCATION>")]
        public string DepartureLocation { get; set; }

        [CommandOption("--location <LOCATION>")]
        public string Location { get; set; }

        [CommandOption("--arrival-location <ARRIVAL-LOCATION>")]
        public string ArrivalLocation { get; set; }

        [CommandOption("--scheduled-arrival-time <ARRIVAL-TIME>")]
        public DateTime ScheduledArrivalTime { get; set; }
    }
}