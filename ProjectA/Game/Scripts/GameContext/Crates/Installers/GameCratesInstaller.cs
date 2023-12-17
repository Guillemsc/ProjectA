using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Crates.Configurations;
using Game.GameContext.Crates.UseCases;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;

namespace Game.GameContext.Crates.Installers;

public static class GameCratesInstaller
{
    public static void InstallGameCrates(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenPlayerCollidedWithCrateUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithCrateUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<SpawnFruitCollectablesWhenCrateHitUseCase>()
            ));

        builder.Bind<SpawnFruitCollectablesWhenCrateHitUseCase>()
            .FromFunction(c => new SpawnFruitCollectablesWhenCrateHitUseCase(
                c.Resolve<GameCratesConfiguration>(),
                c.Resolve<IRandomGenerator>(),
                c.Resolve<SpawnRandomFruitCollectableUseCase>()
            ));
    }
}