using Game.GameContext.Entities.Services;
using Game.GameContext.Saws.UseCases;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Tick.Enums;
using GUtils.Tick.Extensions;

namespace Game.GameContext.Saws.Installers;

public static class GameSawsInstaller
{
    public static void InstallGameSaws(this IDiContainerBuilder builder)
    {
        builder.Bind<TickSawViewsUseCase>()
            .FromFunction(c => new TickSawViewsUseCase(
                c.Resolve<IGameEntitiesService>(),
                c.Resolve<TickSawViewUseCase>()
            ))
            .LinkExecutableToTickablesService(TickType.PhysicsUpdate);

        builder.Bind<TickSawViewUseCase>()
            .FromFunction(c => new TickSawViewUseCase(
                c.Resolve<TickSawMovementUseCase>()
            ));

        builder.Bind<TickSawMovementUseCase>()
            .FromFunction(c => new TickSawMovementUseCase(
                c.Resolve<IGameTimesService>()
            ));
    }
}