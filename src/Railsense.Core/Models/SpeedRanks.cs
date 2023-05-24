using Microsoft.EntityFrameworkCore;

namespace Railsense.Core.Models;

[Owned]
public class SpeedRanks
{
    public int? ARankMax { get; set; }
    public int? ARankMin { get; set; }
    public int? BRankMax { get; set; }
    public int? BRankMin { get; set; }
    public int? CRankMax { get; set; }
    public int? CRankMin { get; set; }
    public int? PRankMax { get; set; }
    public int? PRankMin { get; set; }
    public int? HsRankMax { get; set; }
    public int? HsRankMin { get; set; }
}