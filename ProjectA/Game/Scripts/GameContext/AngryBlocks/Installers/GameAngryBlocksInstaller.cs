using Game.GameContext.AngryBlocks.Configurations;
using Game.GameContext.AngryBlocks.UseCases;
using Game.GameContext.Entities.Services;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;
using GUtils.Tick.Enums;
using GUtils.Tick.Extensions;

namespace Game.GameContext.AngryBlocks.Installers;

public static class GameAngryBlocksInstaller
{
    public static void InstallGameAngryBlocks(this IDiContainerBuilder builder)
    {
        builder.Bind<LoadAngryBlocksUseCase>()
            .FromFunction(c => new LoadAngryBlocksUseCase(
                c.Resolve<IGameEntitiesService>(),
                c.Resolve<WhenAngryBlockCollidedUseCase>()
            ));
        
        builder.Bind<TickAngryBlockViewsUseCase>()
            .FromFunction(c => new TickAngryBlockViewsUseCase(
                c.Resolve<IGameEntitiesService>(),
                c.Resolve<TickAngryBlockViewUseCase>()
            ))
            .LinkExecutableToTickablesService(TickType.PhysicsUpdate);

        builder.Bind<TickAngryBlockViewUseCase>()
            .FromFunction(c => new TickAngryBlockViewUseCase(
                c.Resolve<TickAngryBlockBlinkUseCase>(),
                c.Resolve<TickAngryBlockMovementUseCase>()
            ));

        builder.Bind<TickAngryBlockBlinkUseCase>()
            .FromFunction(c => new TickAngryBlockBlinkUseCase(
                c.Resolve<IGameTimesService>(),
                c.Resolve<IRandomGenerator>(),
                c.Resolve<GameAngryBlocksConfiguration>()
            ));

        builder.Bind<TickAngryBlockMovementUseCase>()
            .FromFunction(c => new TickAngryBlockMovementUseCase(
                c.Resolve<IGameTimesService>()
            ));

        builder.Bind<WhenAngryBlockCollidedUseCase>()
            .FromFunction(c => new WhenAngryBlockCollidedUseCase(
            ));
    }
}