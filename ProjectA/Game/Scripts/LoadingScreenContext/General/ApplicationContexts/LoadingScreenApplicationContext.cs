using Game.Contexts.Configuration;
using Game.LoadingScreenContext.General.Installers;
using Game.LoadingScreenContext.General.Interactors;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;

namespace Game.LoadingScreenContext.General.ApplicationContexts;

public sealed class LoadingScreenApplicationContext : DiApplicationContext<ILoadingScreenContextInteractor>
{
    protected override void Install(IDiContext<ILoadingScreenContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.LoadingScreenContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallLoadingScreenGeneralInteractor();
                b.InstallLoadingScreenGeneralServices();
            }
        ));
    }
}