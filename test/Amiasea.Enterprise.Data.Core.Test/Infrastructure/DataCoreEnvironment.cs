using Microsoft.Extensions.Configuration;

namespace Amiasea.Enterprise.Data.Core.Test.Infrastructure;

public sealed class DataCoreEnvironment
{
    public IConfiguration Configuration { get; } =
        new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["DataCore:ConnectionString"] =
                        "Server=localhost;Database=AmiaseaEnterpriseTest;Trusted_Connection=True;TrustServerCertificate=True"
                })
            .Build();
}