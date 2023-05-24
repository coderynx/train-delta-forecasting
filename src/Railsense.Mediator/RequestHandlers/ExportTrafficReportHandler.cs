using System.Globalization;
using Ardalis.GuardClauses;
using CsvHelper;
using MediatR;
using Railsense.Data.Mappings;
using Railsense.Geospatial.Services;
using Railsense.Mediator.Requests;
using Railsense.Neural.Services;

namespace Railsense.Mediator.RequestHandlers;

public class ExportTrafficReportHandler : IRequestHandler<ExportTrafficReport, Unit>
{
    private readonly IGeospatialService _geospatialService;
    private readonly IMediator _mediator;
    private readonly ITrainDetectionFeaturizer _trainDetectionFeaturizer;

    public ExportTrafficReportHandler(IMediator mediator, IGeospatialService geospatialService,
        ITrainDetectionFeaturizer trainDetectionFeaturizer)
    {
        _mediator = mediator;
        _geospatialService = geospatialService;
        _trainDetectionFeaturizer = trainDetectionFeaturizer;
    }

    public async Task<Unit> Handle(ExportTrafficReport request, CancellationToken cancellationToken)
    {
        // Retrieve train detections.
        var detections = _mediator.Send(request.Request, cancellationToken).Result;
        Guard.Against.NullOrEmpty(detections, nameof(detections));

        switch (request.ExportKind)
        {
            case KindOfExport.Csv:
                await using (var writer = new StreamWriter(request.FilePath))
                await using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (request.IsForArrivalDeltaTraining)
                    {
                        var data = _trainDetectionFeaturizer.Featurize(detections);
                        await csv.WriteRecordsAsync(data, cancellationToken);
                    }
                    else
                    {
                        csv.Context.RegisterClassMap<TrafficReportMapping>();
                        await csv.WriteRecordsAsync(detections, cancellationToken);
                    }
                }

                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new Unit();
    }
}