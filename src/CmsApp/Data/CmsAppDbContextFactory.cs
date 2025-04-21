using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CmsApp.Data;

public class CmsAppDbContextFactory : IDesignTimeDbContextFactory<CmsAppDbContext>
{
    public CmsAppDbContext CreateDbContext(string[] args)
    {
        CmsAppGlobalFeatureConfigurator.Configure();
        var configuration = BuildConfiguration();

        var dbFolder = configuration["App:DbFolderName"]!.EnsureEndsWith(Path.DirectorySeparatorChar);

        var builder = new DbContextOptionsBuilder<CmsAppDbContext>()
            .UseSqlite($"Data Source={dbFolder}{configuration["App:DefaultDbName"]}.db;");

        return new CmsAppDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
