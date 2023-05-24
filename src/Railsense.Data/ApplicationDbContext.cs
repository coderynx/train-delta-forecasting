using Microsoft.EntityFrameworkCore;
using Railsense.Core.Models;

namespace Railsense.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ParsedTrafficSummary> ParsedTrafficSummaries { get; set; }
    public DbSet<Agency> Agencies { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<GeoPoint> GeoPoints { get; set; }
    public DbSet<RouteSegment> RouteSegments { get; set; }
    public DbSet<IgnoredLocation> IgnoredLocations { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<TrainDetection> Traffic { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Data Source=127.0.0.1\SQLEXPRESS,1433; Database=railsense_test; Trusted_Connection=True; Integrated Security = False; User Id=user; Password=123Password");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}