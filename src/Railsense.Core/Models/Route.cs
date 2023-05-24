using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class Route : BaseEntity
{
    public Route(string code, string name)
    {
        Code = code;
        Name = name;
        Sections = new List<RouteSegment>();
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<RouteSegment> Sections { get; set; }
}