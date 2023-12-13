using System.Threading;
using System.Threading.Tasks;
using Game.Contexts.Configuration;
using Game.GameContext.General.ApplicationContexts;
using Game.LoadingScreenContext.General.ApplicationContexts;
using Godot;
using GUtils.ApplicationContexts.Services;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Services.Locators;
using GUtilsGodot.Bootstraps;
using GUtilsGodot.Di.Loadables;

namespace Game.Bootstraps;

public partial class MainBootstrap : Bootstrap
{
    [Export] public ContextsScenesConfiguration? ContextsScenesConfiguration;
    
    protected override async Task Run(CancellationToken cancellationToken)
    {
        ServiceLocator.Register(ContextsScenesConfiguration);
        
        DiContext<int> diContext = new DiContext<int>();
        diContext.AddInstaller(new CallbackInstaller(b => b.Bind<int>().FromInstance(1)));
        diContext.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(ContextsScenesConfiguration!.ServicesContextPrefab, this));
        diContext.Install();

        ILoadingService loadingService = ServiceLocator.Get<ILoadingService>();
        
        await loadingService.New()
            .EnqueueLoadAndStartApplicationContext(new LoadingScreenApplicationContext())
            .Execute(cancellationToken);
        
        await loadingService.New()
            .RunBeforeLoadActionsInstantly()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext())
            .Execute(cancellationToken);
    }
}