using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Connections.UseCases;
using Game.GameContext.Crates.UseCases;
using Game.GameContext.General.Configurations;
using Game.GameContext.General.Datas;
using Game.GameContext.PlayerKillers.UseCases;
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
                c.Resolve<GameApplicationContextConfiguration>(),
                c.Resolve<GamePlayersConfiguration>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GameGeneralViewData>(),
                c.Resolve<GetConnectionWithIdUseCase>(),
                c.Resolve<WhenPlayerStartedCollisionWithWallUseCase>(),
                c.Resolve<WhenPlayerStoppedCollisionWithWallUseCase>(),
                c.Resolve<WhenPlayerStartedInteractionCollisionWithAreaUseCase>(),
                c.Resolve<WhenPlayerStartedInteractionCollisionWithBodyUseCase>()
            ));

        builder.Bind<EnablePlayerUseCase>()
            .FromFunction(c => new EnablePlayerUseCase(
                c.Resolve<PlayerViewData>()
            ));
        
        builder.Bind<AppearPlayerUseCase>()
            .FromFunction(c => new AppearPlayerUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<EnablePlayerUseCase>()
            ));

        builder.Bind<StartPlayerUseCase>()
            .FromFunction(c => new StartPlayerUseCase(
                c.Resolve<GameApplicationContextConfiguration>(),
                c.Resolve<AppearPlayerUseCase>(),
                c.Resolve<EnablePlayerUseCase>()
            ));

        builder.Bind<KillPlayerUseCase>()
            .FromFunction(c => new KillPlayerUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<IAsyncTaskRunner>()
            ));

        builder.Bind<FreezePlayerUseCase>()
            .FromFunction(c => new FreezePlayerUseCase(
                c.Resolve<PlayerViewData>()
            ));
        
        builder.Bind<TickPlayerMovementUseCase>()
            .FromFunction(c => new TickPlayerMovementUseCase(
                c.Resolve<IDeltaTimeService>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GamePlayersConfiguration>(),
                c.Resolve<StorePlayerPreviousPositionUseCase>()
            ))
            .LinkToTickablesService(o => o.Execute, TickType.PhysicsUpdate);

        builder.Bind<TickPlayerFlipStateUseCase>()
            .FromFunction(c => new TickPlayerFlipStateUseCase(
                c.Resolve<PlayerViewData>()
            ))
            .LinkToTickablesService(o => o.Execute);

        builder.Bind<StorePlayerPreviousPositionUseCase>()
            .FromFunction(c => new StorePlayerPreviousPositionUseCase());
        
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
                c.Resolve<WhenPlayerCollidedWithVelocityBoosterUseCase>(),
                c.Resolve<WhenPlayerCollidedWithPlayerKillerUseCase>()
            ));

        builder.Bind<WhenPlayerStartedInteractionCollisionWithBodyUseCase>()
            .FromFunction(c => new WhenPlayerStartedInteractionCollisionWithBodyUseCase(
                c.Resolve<WhenPlayerCollidedWithCrateUseCase>()
            ));
    }
}