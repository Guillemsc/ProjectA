using Game.Contexts.Configuration;
using Game.MetaContext.General.Installers;
using Game.MetaContext.General.Interactors;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;

namespace Game.MetaContext.General.ApplicationContexts;

public sealed class MetaApplicationContext : DiApplicationContext<IMetaContextInteractor>
{
    protected override void Install(IDiContext<IMetaContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.MetaContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallMetaGeneralConfigurations();
                b.InstallMetaGeneralServices();
                b.InstallMetaGeneral();
            }));
    }
}