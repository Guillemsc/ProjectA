using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Crates.UseCases;
using Game.GameContext.General.Datas;
using Game.GameContext.Players.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.UseCases;
using Game.GameContext.Trampolines.UseCases;
using Game.GameContext.VelocityBoosters.UseCases;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;
using GUtils.Tick.Enums;
using GUtils.Tick.Extensions;
using GUtils.Time.Services;

namespace Game.GameContext.Players.Installers;

public static class GamePlayersInstaller
{
    public static void InstallGamePlayers(this IDiContainerBuilder builder)
    {
        builder.Bind<PlayerViewData>().FromNew();
        builder.Bind<PlayerCollidingNodesData>().FromNew();

        builder.Bind<SpawnPlayerUseCase>()
            .FromFunction(c => new SpawnPlayerUseCase(
                c.Resolve<GamePlayersConfiguration>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GameGeneralViewData>(),
                c.Resolve<WhenPlayerStartedCollisionWithWallUseCase>(),
                c.Resolve<WhenPlayerStoppedCollisionWithWallUseCase>(),
                c.Resolve<WhenPlayerStartedInteractionCollisionWithAreaUseCase>(),
                c.Resolve<WhenPlayerStartedInteractionCollisionWithBodyUseCase>()
            ));

        builder.Bind<StartPlayerUseCase>()
            .FromFunction(c => new StartPlayerUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<IAsyncTaskRunner>()
            ));
        
        builder.Bind<TickPlayerMovementUseCase>()
            .FromFunction(c => new TickPlayerMovementUseCase(
                c.Resolve<IDeltaTimeService>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GamePlayersConfiguration>()
            ))
            .LinkToTickablesService(o => o.Execute, TickType.PhysicsUpdate);

        builder.Bind<TickPlayerFlipStateUseCase>()
            .FromFunction(c => new TickPlayerFlipStateUseCase(
                c.Resolve<PlayerViewData>()
            ))
            .LinkToTickablesService(o => o.Execute);

        builder.Bind<WhenPlayerStartedCollisionWithWallUseCase>()
            .FromFunction(c => new WhenPlayerStartedCollisionWithWallUseCase(
                c.Resolve<PlayerViewData>()
            ));

        builder.Bind<WhenPlayerStoppedCollisionWithWallUseCase>()
            .FromFunction(c => new WhenPlayerStoppedCollisionWithWallUseCase(
                c.Resolve<PlayerViewData>()
            ));

        builder.Bind<WhenPlayerStartedInteractionCollisionWithAreaUseCase>()
            .FromFunction(c => new WhenPlayerStartedInteractionCollisionWithAreaUseCase(
                c.Resolve<WhenPlayerCollidedWithCollectableUseCase>(),
                c.Resolve<WhenPlayerCollidedWithTrampolineUseCase>(),
                c.Resolve<WhenPlayerCollidedWithVelocityBoosterUseCase>()
            ));

        builder.Bind<WhenPlayerStartedInteractionCollisionWithBodyUseCase>()
            .FromFunction(c => new WhenPlayerStartedInteractionCollisionWithBodyUseCase(
                c.Resolve<WhenPlayerCollidedWithCrateUseCase>()
            ));
    }
}