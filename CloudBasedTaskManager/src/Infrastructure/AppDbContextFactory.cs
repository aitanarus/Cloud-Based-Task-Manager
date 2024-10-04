using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    // Factory class that provides the configuration for migrations at design time.
    // Since Infrastructure is a class library, EF Core will not automatically be able to find the DbContext configuration in the WebAPI project.
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Set up the path to the WebAPI project directory
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebAPI");

            // Build the configuration directly from the appsettings.json without SetBasePath
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(basePath, "appsettings.json")) // Provide full path
                .Build();

            // Read the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Create the DbContextOptionsBuilder and use SQL Server with the connection string
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
