using System.Threading;
using System.Threading.Tasks;
using Game.Contexts.Configuration;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.LoadingScreenContext.General.ApplicationContexts;
using Game.MetaContext.General.ApplicationContexts;
using Godot;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Directions;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Services.Locators;
using GUtilsGodot.Bootstraps;
using GUtilsGodot.Di.Loadables;

namespace Game.Bootstraps;

public partial class MainBootstrap : Bootstrap
{
    [Export] public ContextsScenesConfiguration? ContextsScenesConfiguration;
    [Export] public GameConfiguration? GameConfiguration;
    
    protected override async Task Run(CancellationToken cancellationToken)
    {
        ServiceLocator.Register(ContextsScenesConfiguration);
        ServiceLocator.Register(GameConfiguration);
        
        DiContext<int> diContext = new DiContext<int>();
        diContext.AddInstaller(new CallbackInstaller(b => b.Bind<int>().FromInstance(1)));
        diContext.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(ContextsScenesConfiguration!.ServicesContextPrefab!, this));
        diContext.Install();

        ILoadingService loadingService = ServiceLocator.Get<ILoadingService>();
        
        await loadingService.New()
            .EnqueueLoadAndStartApplicationContext(new LoadingScreenApplicationContext())
            .Execute(cancellationToken);

        await loadingService.New()
            .EnqueueLoadAndStartApplicationContext(new MetaApplicationContext())
            .Execute(cancellationToken);

        // GameApplicationContextConfiguration contextConfiguration = new(
        //     GameConfiguration!.MapsConfiguration!.TestMap!,
        //     string.Empty,
        //     true,
        //     HorizontalDirection.Right
        // );
        //
        // await loadingService.New()
        //     .RunBeforeLoadActionsInstantly()
        //     .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
        //     .Execute(cancellationToken);
    }
}