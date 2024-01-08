using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.Configurations;
using Game.LoadingScreenContext.General.ApplicationContexts;
using Godot;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using ContextsScenesConfiguration = Game.General.Contexts.Configuration.ContextsScenesConfiguration;

namespace Game.Bootstraps;

public static class CoreBootstrapSetup
{ 
    public static async Task Execute(
        Node node,
        ContextsScenesConfiguration contextsScenesConfiguration,
        GameConfiguration gameConfiguration,
        CancellationToken cancellationToken
        )
    {
        ServiceLocator.Register(contextsScenesConfiguration);
        ServiceLocator.Register(gameConfiguration);
        
        DiContext<int> diContext = new DiContext<int>();
        diContext.AddInstaller(new CallbackInstaller(b => b.Bind<int>().FromInstance(1)));
        diContext.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration!.ServicesContextPrefab!, node));
        diContext.Install();

        ILoadingService loadingService = ServiceLocator.Get<ILoadingService>();
        
        await loadingService.New()
            .EnqueueLoadAndStartApplicationContext(new LoadingScreenApplicationContext())
            .Execute(cancellationToken);
    }
}