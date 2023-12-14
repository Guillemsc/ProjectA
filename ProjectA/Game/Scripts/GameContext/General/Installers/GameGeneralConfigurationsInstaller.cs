using Game.GameContext.General.Configurations;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Players.Configurations;
using Game.GameContext.VisualEffects.Configurations;
using GUtils.Di.Builder;

namespace Game.GameContext.General.Installers;

public static class GameGeneralConfigurationsInstaller
{
    public static void InstallGameGeneralConfigurations(this IDiContainerBuilder builder, GameConfiguration gameConfiguration)
    {
        builder.Bind<GameConfiguration>().FromInstance(gameConfiguration);
        builder.Bind<GamePlayersConfiguration>().FromInstance(gameConfiguration.PlayersConfiguration!);
        builder.Bind<GameMapsConfiguration>().FromInstance(gameConfiguration.MapsConfiguration!);
        builder.Bind<GameVisualEffectsConfiguration>().FromInstance(gameConfiguration.VisualEffectsConfiguration!);
    }
}