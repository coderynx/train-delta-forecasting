using Ardalis.GuardClauses;
using MediatR;

namespace Railsense.Mediator.Requests;

public interface IExportTrafficReport
{
    public IExportTrafficReport WithQuery(QueryTrainDetectionsRequest queryTrainDetectionsRequest);
    public IExportTrafficReport ForArrivalDeltaTraining();
    public IExportTrafficReport ExportAsCSsv();
    public IExportTrafficReport WithFilePath(string filePath);
    public ExportTrafficReport Build();
}

public enum KindOfExport
{
    Csv
}

public class ExportTrafficReport : IExportTrafficReport, IRequest<Unit>
{
    public QueryTrainDetectionsRequest Request { get; set; }
    public bool IsForArrivalDeltaTraining { get; set; }
    public KindOfExport? ExportKind { get; set; }
    public string FilePath { get; set; }

    public IExportTrafficReport WithQuery(QueryTrainDetectionsRequest queryTrainDetectionsRequest)
    {
        Request = Guard.Against.Null(queryTrainDetectionsRequest, nameof(queryTrainDetectionsRequest));
        return this;
    }

    public IExportTrafficReport ForArrivalDeltaTraining()
    {
        IsForArrivalDeltaTraining = true;
        return this;
    }

    public IExportTrafficReport ExportAsCSsv()
    {
        ExportKind = KindOfExport.Csv;
        return this;
    }

    public IExportTrafficReport WithFilePath(string filePath)
    {
        FilePath = Guard.Against.NullOrWhiteSpace(filePath, nameof(filePath));
        return this;
    }

    public ExportTrafficReport Build()
    {
        // Checks if all the parameters are set.
        Guard.Against.Null(Request, nameof(Request));
        Guard.Against.NullOrWhiteSpace(FilePath, nameof(FilePath));
        Guard.Against.Null(ExportKind, nameof(ExportKind));

        return this;
    }
}