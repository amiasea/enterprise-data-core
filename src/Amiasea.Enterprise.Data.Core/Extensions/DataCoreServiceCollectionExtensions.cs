using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Amiasea.Enterprise.Data.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataCoreServiceCollectionExtensions
{
  public static IServiceCollection AddCoreData(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services
        .AddOptions<DataCoreOptions>()
        .Bind(configuration.GetSection("DataCore"))
        .Validate(
            options => !string.IsNullOrWhiteSpace(options.ConnectionString),
            "DataCore:ConnectionString is required.")
        .ValidateOnStart();

    services.AddDbContext<CoreDbContext>((serviceProvider, options) =>
    {
      var dataOptions = serviceProvider
              .GetRequiredService<IOptions<DataCoreOptions>>()
              .Value;

      options.UseSqlServer(dataOptions.ConnectionString);
    });

    services.AddCoreDataLoaders();

    return services;
  }
}