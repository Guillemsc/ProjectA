using Game.GameContext.Cinematics.Datas;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Dialogues.UseCases;
using Game.GameContext.General.Configurations;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Cinematics.Installers;

public static class GameCinematicsInstaller
{
    public static void InstallGameCinematics(this IDiContainerBuilder builder)
    {
        builder.Bind<CurrentCinematicData>().FromNew();
        
        builder.Bind<CinematicsMethods>()
            .FromFunction(c => new CinematicsMethods(
                c.Resolve<AwaitUntilPlayerIsOnTheGroundUseCase>(),
                c.Resolve<PlayDialogueUseCase>()
            ));
        
        builder.Bind<WhenPlayerCollidedWithCinematicTriggerUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithCinematicTriggerUseCase(
                c.Resolve<PlayCinematicUseCase>()
            ));

        builder.Bind<PlayCinematicUseCase>()
            .FromFunction(c => new PlayCinematicUseCase(
                c.Resolve<CurrentCinematicData>(),
                c.Resolve<GameConfiguration>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<CinematicsMethods>(),
                c.Resolve<IAsyncTaskRunner>()
            ));

        builder.Bind<SkipCurrentCinematicUseCase>()
            .FromFunction(c => new SkipCurrentCinematicUseCase(
                c.Resolve<CurrentCinematicData>()
            ));

        builder.Bind<HasStartingMapCinematicUseCase>()
            .FromFunction(c => new HasStartingMapCinematicUseCase(
                c.Resolve<MapViewData>()
            ));
        
        builder.Bind<TryPlayStartingMapCinematicUseCase>()
            .FromFunction(c => new TryPlayStartingMapCinematicUseCase(
                c.Resolve<MapViewData>(),
                c.Resolve<PlayCinematicUseCase>()
            ));
        
        builder.Bind<AwaitUntilPlayerIsOnTheGroundUseCase>()
            .FromFunction(c => new AwaitUntilPlayerIsOnTheGroundUseCase(
                c.Resolve<PlayerViewData>()
            ));
    }
}