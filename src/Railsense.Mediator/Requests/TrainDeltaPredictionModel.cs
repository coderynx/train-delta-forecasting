using MediatR;

namespace Railsense.Mediator.Requests;

public class TrainDeltaPredictionModel : IRequest<Unit>
{
    public TrainDeltaPredictionModel(QueryTrainDetectionsRequest datasetRequest)
    {
        DatasetRequest = datasetRequest;
    }

    public QueryTrainDetectionsRequest DatasetRequest { get; set; }
}