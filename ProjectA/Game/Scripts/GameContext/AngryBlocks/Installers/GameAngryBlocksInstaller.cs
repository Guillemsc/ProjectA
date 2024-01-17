using Game.GameContext.AngryBlocks.Configurations;
using Game.GameContext.AngryBlocks.UseCases;
using Game.GameContext.Entities.Services;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Pooling.Extensions;
using GUtils.Randomization.Generators;
using GUtils.Tick.Enums;
using GUtils.Tick.Extensions;

namespace Game.GameContext.AngryBlocks.Installers;

public static class GameAngryBlocksInstaller
{
    public static void InstallGameAngryBlocks(this IDiContainerBuilder builder)
    {
        builder.Bind<LoadAngryBlocksUseCase>()
            .FromAutoPool((c, o) => o.Init(
                c.Resolve<IGameEntitiesService>(),
                c.Resolve<WhenAngryBlockCollidedUseCase>(),
                c.Resolve<RefreshAngryBlockActiveCollidersUseCase>()
            ));
        
        builder.Bind<TickAngryBlockViewsUseCase>()
            .FromAutoPool((c, o) => o.Init(
                c.Resolve<IGameEntitiesService>(),
                c.Resolve<TickAngryBlockViewUseCase>()
            ))
            .LinkExecutableToTickablesService(TickType.PhysicsUpdate);
        
        builder.Bind<TickAngryBlockViewUseCase>()
            .FromAutoPool((c, o) => o.Init(
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
        
        builder.Bind<RefreshAngryBlockActiveCollidersUseCase>()
            .FromAutoPool();
        
        builder.Bind<WhenAngryBlockCollidedUseCase>()
            .FromFunction(c => new WhenAngryBlockCollidedUseCase(
                c.Resolve<RefreshAngryBlockActiveCollidersUseCase>()
            ));
    }
}