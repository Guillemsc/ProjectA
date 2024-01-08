using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.Configurations;
using Game.MetaContext.General.ApplicationContexts;
using Godot;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Services.Locators;
using GUtilsGodot.Bootstraps;
using ContextsScenesConfiguration = Game.General.Contexts.Configuration.ContextsScenesConfiguration;

namespace Game.Bootstraps;

public partial class MainBootstrap : Bootstrap
{
    [Export] public ContextsScenesConfiguration? ContextsScenesConfiguration;
    [Export] public GameConfiguration? GameConfiguration;
    
    protected override async Task Run(CancellationToken cancellationToken)
    {
        await CoreBootstrapSetup.Execute(
            this,
            ContextsScenesConfiguration!,
            GameConfiguration!,
            cancellationToken
        );

        ILoadingService loadingService = ServiceLocator.Get<ILoadingService>();

        await loadingService.New()
            .EnqueueLoadAndStartApplicationContext(new MetaApplicationContext())
            .Execute(cancellationToken);
    }
}