using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Amiasea.Enterprise.Data.Core.Test.Infrastructure;

namespace Amiasea.Enterprise.Data.Core.Test;

public sealed class DataCoreConfigurationTest
{
    [Fact]
    public async Task AddCoreData_WithValidConfiguration_StartsSuccessfully()
    {
        var environment = new DataCoreEnvironment();

        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddCoreData(environment.Configuration);

        using var host = builder.Build();

        await host.StartAsync();
        await host.StopAsync();
    }

    [Fact]
    public async Task AddCoreData_WithoutConnectionString_ThrowsOnStartup()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddCoreData(
            new DataCoreEnvironmentWithoutConnectionString().Configuration);

        using var host = builder.Build();

        await Assert.ThrowsAsync<OptionsValidationException>(
            () => host.StartAsync());
    }

    private sealed class DataCoreEnvironmentWithoutConnectionString
    {
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; } =
            new Microsoft.Extensions.Configuration.ConfigurationManager();
    }
}