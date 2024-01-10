using Game.MetaContext.General.Configurations;
using Game.MetaContext.General.Installers;
using Game.MetaContext.General.Interactors;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;
using ContextsScenesConfiguration = Game.General.Contexts.Configuration.ContextsScenesConfiguration;

namespace Game.MetaContext.General.ApplicationContexts;

public sealed class MetaApplicationContext : DiApplicationContext<IMetaContextInteractor>
{
    readonly MetaApplicationContextConfiguration _contextConfiguration;

    public MetaApplicationContext(
        MetaApplicationContextConfiguration contextConfiguration
        )
    {
        _contextConfiguration = contextConfiguration;
    }

    protected override void Install(IDiContext<IMetaContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.MetaContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallMetaGeneralConfigurations(_contextConfiguration);
                b.InstallMetaGeneralServices();
                b.InstallMetaGeneral();
            }));
    }
}