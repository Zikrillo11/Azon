
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Azon.DAL.DAL;

namespace Azon.DAL.DAL;

public sealed class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var directory = new DirectoryInfo(currentDirectory);

        string? configurationPath = null;

        while (directory is not null)
        {
            var apiPath = Path.Combine(
                directory.FullName,
                "Azon.API",
                "appsettings.json");

            var currentPath = Path.Combine(
                directory.FullName,
                "appsettings.json");

            if (File.Exists(apiPath))
            {
                configurationPath = Path.GetDirectoryName(apiPath);
                break;
            }

            if (File.Exists(currentPath) &&
                File.Exists(Path.Combine(
                    directory.FullName,
                    "Azon.API",
                    "Azon.API.csproj")))
            {
                configurationPath = directory.FullName;
                break;
            }

            directory = directory.Parent;
        }

        if (configurationPath is null)
        {
            throw new FileNotFoundException(
                "Azon.API/appsettings.json topilmadi.");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configurationPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "DefaultConnection topilmadi.");
        }

        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
