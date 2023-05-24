using MediatR;
using Railsense.Core.Models;

namespace Railsense.Mediator.Requests;

public interface ITrainDetectionsQueryRequest
{
    ITrainDetectionsQueryRequest WithAgency(string agency);
    ITrainDetectionsQueryRequest WithServiceCategory(string? serviceCategory);
    ITrainDetectionsQueryRequest WithServiceCategoryKind(ServiceCategoryKind? kind);
    ITrainDetectionsQueryRequest WithLocation(string? node);
    ITrainDetectionsQueryRequest WithDepartureLocation(string? departureNode);
    ITrainDetectionsQueryRequest WithArrivalLocation(string? arrivalNode);
    ITrainDetectionsQueryRequest WithMaxArrivalDelta(int? value);
    ITrainDetectionsQueryRequest WithMinArrivalDelta(int? value);
    QueryTrainDetectionsRequest Build();
}

public class QueryTrainDetectionsRequest : IRequest<IEnumerable<TrainDetection>>, ITrainDetectionsQueryRequest
{
    public QueryTrainDetectionsRequest()
    {
        Agency = string.Empty;
        ServiceCategory = string.Empty;
        Node = string.Empty;
        DepartureNode = string.Empty;
        ArrivalNode = string.Empty;
    }

    public string Agency { get; set; }
    public string ServiceCategory { get; set; }
    public ServiceCategoryKind? ServiceCategoryKind { get; set; }
    public string Node { get; set; }
    public string DepartureNode { get; set; }
    public string ArrivalNode { get; set; }
    public int? MaxArrivalDelta { get; set; }
    public int? MinArrivalDelta { get; set; }

    public ITrainDetectionsQueryRequest WithAgency(string? agency)
    {
        if (agency is not null && !agency.Equals(" ")) Agency = agency;
        return this;
    }

    public ITrainDetectionsQueryRequest WithServiceCategory(string? serviceCategory)
    {
        if (serviceCategory is not null && !serviceCategory.Equals(" ")) ServiceCategory = serviceCategory;
        return this;
    }

    public ITrainDetectionsQueryRequest WithServiceCategoryKind(ServiceCategoryKind? kind)
    {
        if (kind is not null) ServiceCategoryKind = kind;
        return this;
    }

    public ITrainDetectionsQueryRequest WithLocation(string? node)
    {
        if (node is not null && !node.Equals(" ")) Node = node;
        return this;
    }

    public ITrainDetectionsQueryRequest WithDepartureLocation(string? departureNode)
    {
        if (departureNode is not null && !departureNode.Equals(" ")) DepartureNode = departureNode;
        return this;
    }

    public ITrainDetectionsQueryRequest WithArrivalLocation(string? arrivalNode)
    {
        if (arrivalNode is not null && !arrivalNode.Equals(" ")) ArrivalNode = arrivalNode;
        return this;
    }

    public ITrainDetectionsQueryRequest WithMaxArrivalDelta(int? value)
    {
        MaxArrivalDelta = value;
        return this;
    }

    public ITrainDetectionsQueryRequest WithMinArrivalDelta(int? value)
    {
        MinArrivalDelta = value;
        return this;
    }

    public QueryTrainDetectionsRequest Build()
    {
        return this;
    }
}