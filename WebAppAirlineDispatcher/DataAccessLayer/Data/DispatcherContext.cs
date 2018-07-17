using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class DispatcherContext:DbContext
    {
        public DispatcherContext(DbContextOptions<DispatcherContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Stewardess> Stewardesses { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<PlaneType> PlaneTypes { get; set; }
    }
}
