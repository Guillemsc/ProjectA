using Game.GameContext.General.Configurations;
using GUtils.Di.Builder;
using GUtils.Services.Extensions;

namespace Game.MetaContext.General.Installers;

public static class MetaGeneralConfigurationInstaller
{
    public static void InstallMetaGeneralConfigurations(
        this IDiContainerBuilder builder
    )
    {
        builder.Bind<GameConfiguration>().FromServiceLocator();
    }
}