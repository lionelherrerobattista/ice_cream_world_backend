using Microsoft.EntityFrameworkCore;

namespace ice_cream_world_backend.models
{
    public class IceCreamWorldContext : DbContext
    {
        public DbSet<Flavor> Flavors { get; set; } = null!;
        public DbSet<ServingContainer> ServingContainers { get; set; } = null!;

        // public string DbPath { get; }

        public IceCreamWorldContext(DbContextOptions options) : base(options)
        {
            // var folder = Environment.SpecialFolder.LocalApplicationData;
            // var path = Environment.GetFolderPath(folder);

            // DbPath = System.IO.Path.Join(path, "iceCreamWorld.db");
        }
    }
}