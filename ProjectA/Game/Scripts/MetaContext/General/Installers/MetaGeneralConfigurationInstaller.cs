using Game.GameContext.General.Configurations;
using Game.MetaContext.General.Configurations;
using GUtils.Di.Builder;
using GUtils.Services.Extensions;

namespace Game.MetaContext.General.Installers;

public static class MetaGeneralConfigurationInstaller
{
    public static void InstallMetaGeneralConfigurations(
        this IDiContainerBuilder builder,
        MetaApplicationContextConfiguration contextConfiguration
    )
    {
        builder.Bind<MetaApplicationContextConfiguration>().FromInstance(contextConfiguration);
        builder.Bind<GameConfiguration>().FromServiceLocator();
    }
}