// CoreWolverineExtension.cs
using Wolverine;
using Amiasea.Enterprise.Data.Core;

public sealed class CoreWolverineExtension : IWolverineExtension
{
    public void Configure(WolverineOptions options)
    {
        options.CodeGeneration
            .AlwaysUseServiceLocationFor<CoreDbContext>();
    }
}