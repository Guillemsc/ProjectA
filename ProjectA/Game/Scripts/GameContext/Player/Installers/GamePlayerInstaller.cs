using Game.GameContext.General.Datas;
using Game.GameContext.Player.Configurations;
using Game.GameContext.Player.Datas;
using Game.GameContext.Player.UseCases;
using GUtils.Di.Builder;
using GUtils.Tick.Enums;
using GUtils.Tick.Extensions;
using GUtils.Time.Services;

namespace Game.GameContext.Player.Installers;

public static class GamePlayerInstaller
{
    public static void InstallGamePlayer(this IDiContainerBuilder builder)
    {
        builder.Bind<PlayerViewData>().FromNew();

        builder.Bind<SpawnPlayerUseCase>()
            .FromFunction(c => new SpawnPlayerUseCase(
                c.Resolve<GamePlayerConfiguration>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GameGeneralViewData>(),
                c.Resolve<WhenPlayerStartedCollisionWithWallUseCase>(),
                c.Resolve<WhenPlayerStoppedCollisionWithWallUseCase>()
            ));

        builder.Bind<TickPlayerMovementUseCase>()
            .FromFunction(c => new TickPlayerMovementUseCase(
                c.Resolve<IDeltaTimeService>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<GamePlayerConfiguration>()
            ))
            .LinkToTickablesService(o => o.Execute, TickType.PhysicsUpdate);

        builder.Bind<TickPlayerAnimationsUseCase>()
            .FromFunction(c => new TickPlayerAnimationsUseCase(
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
    }
}