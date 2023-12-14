using Game.GameContext.General.Configurations;
using Game.GameContext.Player.Configurations;
using GUtils.Di.Builder;

namespace Game.GameContext.General.Installers;

public static class GameGeneralConfigurationsInstaller
{
    public static void InstallGameGeneralConfigurations(this IDiContainerBuilder builder, GameConfiguration gameConfiguration)
    {
        builder.Bind<GameConfiguration>().FromInstance(gameConfiguration);
        builder.Bind<GamePlayerConfiguration>().FromInstance(gameConfiguration.PlayerConfiguration!);
    }
}