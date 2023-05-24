using Railsense.Core.Models.Base;

namespace Railsense.Core.Models;

public class IgnoredLocation : BaseEntity
{
    public IgnoredLocation(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}