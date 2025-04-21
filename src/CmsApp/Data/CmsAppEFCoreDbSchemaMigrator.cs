using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace CmsApp.Data;

public class CmsAppEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public CmsAppEFCoreDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync(string? connectionString = null)
    {
        if (connectionString.IsNullOrWhiteSpace())
        {
            await _serviceProvider.GetRequiredService<CmsAppDbContext>().Database.MigrateAsync();
            return;
        }

        var options = new DbContextOptionsBuilder<CmsAppDbContext>()
                .UseSqlite(connectionString)
                .Options;

        using (var dbContext = new CmsAppDbContext(options))
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}