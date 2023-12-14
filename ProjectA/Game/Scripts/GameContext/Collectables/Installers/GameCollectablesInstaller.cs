using Game.GameContext.Collectables.UseCases;
using Game.GameContext.VisualEffects.UseCases;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Collectables.Installers;

public static class GameCollectablesInstaller
{
    public static void InstallGameCollectables(this IDiContainerBuilder builder)
    {
        builder.Bind<CollectCollectableUseCase>()
            .FromFunction(c => new CollectCollectableUseCase(
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<PlayOneShotVisualEffectUseCase>()
                ));
    }
}