using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class ParsedTrafficSummary : BaseEntity
{
    public ParsedTrafficSummary(string location, DateTime creationDate)
    {
        Location = location;
        CreationDate = creationDate;
    }

    public string Location { get; set; }
    public DateTime CreationDate { get; set; }
}