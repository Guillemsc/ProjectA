using Game.GameContext.Collectables.Configurations;
using Game.GameContext.Collectables.Datas;
using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Maps.Datas;
using Game.GameContext.VisualEffects.UseCases;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Collectables.Installers;

public static class GameCollectablesInstaller
{
    public static void InstallGameCollectables(this IDiContainerBuilder builder)
    {
        builder.Bind<CollectablesPrefabsData>().FromNew();

        builder.Bind<RegisterCollectablesPrefabsUseCase>()
            .FromFunction(c => new RegisterCollectablesPrefabsUseCase(
                c.Resolve<GameCollectablesConfiguration>(),
                c.Resolve<CollectablesPrefabsData>()
            ))
            .WhenInit(o => o.Execute)
            .NonLazy();

        builder.Bind<WhenPlayerCollidedWithCollectableUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithCollectableUseCase(
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<PlayOneShotVisualEffectUseCase>()
            ));

        builder.Bind<SpawnRandomFruitCollectableUseCase>()
            .FromFunction(c => new SpawnRandomFruitCollectableUseCase(
                c.Resolve<CollectablesPrefabsData>(),
                c.Resolve<MapViewData>(),
                c.Resolve<IRandomGenerator>()
            ));
    }
}