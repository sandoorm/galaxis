using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GalaxisProjectWebAPI.Infrastructure
{
    public class GalaxisDbContextFactory : IDesignTimeDbContextFactory<GalaxisDbContext>
    {
        public GalaxisDbContextFactory()
        {
        }

        public GalaxisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GalaxisDbContext>();
            string connectionString = "User Id=galaxis;Password=galaxis;Server=localhost;Port=5432;Database=galaxis;";
            optionsBuilder.UseNpgsql(connectionString);

            return new GalaxisDbContext(optionsBuilder.Options);
        }
    }
}