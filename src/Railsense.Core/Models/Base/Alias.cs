using Microsoft.EntityFrameworkCore;

namespace Railsense.Core.Models.Base;

[Owned]
public class Alias
{
    public Alias(string name = null!)
    {
        Name = name ?? throw new ArgumentException("Invalid alias");
    }

    public string Name { get; set; }
}