namespace Railsense.Core.Models.Base;

/// <summary>
///     A entity related to the railway (nodes, agencies and service categories).
/// </summary>
public abstract class RailwayEntity : BaseEntity
{
    public RailwayEntity()
    {
        Aliases = new List<Alias>();
    }

    /// <summary>
    ///     The unique name of the railway object
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Aliases of the railway entity.
    /// </summary>
    public ICollection<Alias> Aliases { get; set; }
}