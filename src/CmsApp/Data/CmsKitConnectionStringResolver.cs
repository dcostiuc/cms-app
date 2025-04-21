﻿using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace CmsApp.Data;

[Dependency(ReplaceServices = true)]
public class CmsKitConnectionStringResolver : DefaultConnectionStringResolver
{
    private readonly CmsAppUserIdResolver _demoNameResolver;
    private readonly IConfiguration _configuration;
    
    public CmsKitConnectionStringResolver(
        IOptionsMonitor<AbpDbConnectionOptions> options,
        CmsAppUserIdResolver demoNameResolver,
        IConfiguration configuration)
        : base(options)
    {
        _demoNameResolver = demoNameResolver;
        _configuration = configuration;
    }
    
    [Obsolete("Use ResolveAsync method.")]
    public override string Resolve(string connectionStringName = null)
    {
        return AsyncHelper.RunSync(() => ResolveInternalAsync(connectionStringName));
    }

    public async override Task<string> ResolveAsync(string connectionStringName = null)
    {
        return await ResolveInternalAsync(connectionStringName);
    }

    private async Task<string> ResolveInternalAsync(string connectionStringName = null)
    {
        var dbFolder = _configuration["App:DbFolderName"]?.EnsureEndsWith(Path.DirectorySeparatorChar);
        if (dbFolder.IsNullOrWhiteSpace())
        {
            return await base.ResolveAsync(connectionStringName);
        }

        var demoUserId = _demoNameResolver.GetDemoUserIdOrNull() ?? _configuration["App:DefaultDbName"];

        var dbFilePath = $"{dbFolder}{demoUserId}.db";
        if (!File.Exists(dbFilePath))
        {            
            File.Copy(_configuration["App:DbFolderName"]?.EnsureEndsWith(Path.DirectorySeparatorChar) + _configuration["App:DefaultDbName"] + ".db", dbFilePath);
        }

        
        return $"Data Source={dbFilePath};Cache=Shared";
    }
}