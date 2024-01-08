using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Collectables.Configurations;
using Game.GameContext.Crates.Configurations;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.General.Configurations;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Pause.Configurations;
using Game.GameContext.Players.Configurations;
using Game.GameContext.VisualEffects.Configurations;
using GUtils.Di.Builder;

namespace Game.GameContext.General.Installers;

public static class GameGeneralConfigurationsInstaller
{
    public static void InstallGameGeneralConfigurations(
        this IDiContainerBuilder builder, 
        GameApplicationContextConfiguration contextConfiguration,
        GameConfiguration gameConfiguration
        )
    {
        builder.Bind<GameApplicationContextConfiguration>().FromInstance(contextConfiguration);
        builder.Bind<GameConfiguration>().FromInstance(gameConfiguration);
        builder.Bind<GamePlayersConfiguration>().FromInstance(gameConfiguration.PlayersConfiguration!);
        builder.Bind<GameMapsConfiguration>().FromInstance(gameConfiguration.MapsConfiguration!);
        builder.Bind<GameCamerasConfiguration>().FromInstance(gameConfiguration.CamerasConfiguration!);
        builder.Bind<GameVisualEffectsConfiguration>().FromInstance(gameConfiguration.VisualEffectsConfiguration!);
        builder.Bind<GameCollectablesConfiguration>().FromInstance(gameConfiguration.CollectablesConfiguration!);
        builder.Bind<GameCratesConfiguration>().FromInstance(gameConfiguration.CratesConfiguration!);
        builder.Bind<GameDialoguesConfiguration>().FromInstance(gameConfiguration.DialoguesConfiguration!);
        builder.Bind<GamePauseConfiguration>().FromInstance(gameConfiguration.GamePauseConfiguration!);
    }
}