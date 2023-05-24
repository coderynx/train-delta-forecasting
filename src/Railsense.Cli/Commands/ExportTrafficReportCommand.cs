using System.ComponentModel;
using MediatR;
using Railsense.Mediator.Requests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Railsense.Cli.Commands;

internal sealed class ExportTrafficReportCommand : Command<ExportTrafficReportCommand.Settings>
{
    private readonly IMediator _mediator;

    public ExportTrafficReportCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.Render(new Rule("Query train detections"));

        // Composes the query request.
        var queryRequest = new QueryTrainDetectionsRequest()
            .WithAgency(settings.Agency)
            .WithServiceCategory(settings.ServiceCategory)
            .WithLocation(settings.Node)
            .WithDepartureLocation(settings.DepartureNode)
            .WithArrivalLocation(settings.ArrivalNode);

        // Composes the export request.
        IExportTrafficReport exportRequest = new ExportTrafficReport();

        if (settings.IsForDelayPredictionTraining.Equals(true))
        {
            queryRequest = queryRequest
                .WithMinArrivalDelta(-600)
                .WithMaxArrivalDelta(600);

            exportRequest = exportRequest.ForArrivalDeltaTraining();
        }

        if (settings.Format.ToLower().Equals("csv")) exportRequest = exportRequest.ExportAsCSsv();

        exportRequest = exportRequest.WithQuery(queryRequest.Build())
            .WithFilePath(settings.FilePath)
            .Build();

        AnsiConsole.MarkupLine("Exporting [yellow]traffic report[/]...");

        // Sends the request.
        var result = _mediator.Send(exportRequest).Result;

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [Description("The agency of the train service.")]
        [CommandOption("--agency")]
        public string? Agency { get; init; } = null!;

        [Description("The category of the train service")]
        [CommandOption("--category")]
        public string? ServiceCategory { get; set; } = null!;

        [Description("The node where the train was detected")]
        [CommandOption("--node")]
        public string? Node { get; set; }

        [Description("The node where the train departed")]
        [CommandOption("--departure-node")]
        public string? DepartureNode { get; set; }

        [Description("The node where the train arrived.")]
        [CommandOption("--arrival-node")]
        public string? ArrivalNode { get; set; }

        [Description("Exports the traffic report for traing the delta prediction.")]
        [CommandOption("--delta-prediction")]
        public bool IsForDelayPredictionTraining { get; set; }

        [Description("The export file format.")]
        [CommandOption("--format")]
        public string Format { get; set; }

        [Description("The output file path.")]
        [CommandOption("--path")]
        public string FilePath { get; set; }
    }
}