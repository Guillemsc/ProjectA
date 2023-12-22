using Game.GameContext.Cinematics.Datas;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Cinematics.Installers;

public static class GameCinematicsInstaller
{
    public static void InstallGameCinematics(this IDiContainerBuilder builder)
    {
        builder.Bind<CinematicsMethods>()
            .FromFunction(c => new CinematicsMethods(
                c.Resolve<AwaitUntilPlayerIsOnTheGroundUseCase>()
            ));
        
        builder.Bind<WhenPlayerCollidedWithCinematicTriggerUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithCinematicTriggerUseCase(
                c.Resolve<PlayCinematicUseCase>()
            ));

        builder.Bind<PlayCinematicUseCase>()
            .FromFunction(c => new PlayCinematicUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<CinematicsMethods>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<AwaitUntilPlayerIsOnTheGroundUseCase>()
            ));

        builder.Bind<TryPlayMapOptionalStartingCinematicUseCase>()
            .FromFunction(c => new TryPlayMapOptionalStartingCinematicUseCase(
                c.Resolve<MapViewData>(),
                c.Resolve<PlayCinematicUseCase>()
            ));
        
        builder.Bind<AwaitUntilPlayerIsOnTheGroundUseCase>()
            .FromFunction(c => new AwaitUntilPlayerIsOnTheGroundUseCase(
                c.Resolve<PlayerViewData>()
            ));
    }
}