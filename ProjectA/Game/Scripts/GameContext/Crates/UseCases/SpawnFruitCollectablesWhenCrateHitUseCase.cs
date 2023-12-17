using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Collectables.Views;
using Game.GameContext.Crates.Configurations;
using Game.GameContext.Crates.Views;
using Godot;
using GUtils.Optionals;
using GUtils.Randomization.Generators;

namespace Game.GameContext.Crates.UseCases;

public sealed class SpawnFruitCollectablesWhenCrateHitUseCase
{
    readonly GameCratesConfiguration _gameCratesConfiguration;
    readonly IRandomGenerator _randomGenerator;
    readonly SpawnRandomFruitCollectableUseCase _spawnRandomFruitCollectableUseCase;

    public SpawnFruitCollectablesWhenCrateHitUseCase(
        GameCratesConfiguration gameCratesConfiguration,
        IRandomGenerator randomGenerator,
        SpawnRandomFruitCollectableUseCase spawnRandomFruitCollectableUseCase
        )
    {
        _gameCratesConfiguration = gameCratesConfiguration;
        _randomGenerator = randomGenerator;
        _spawnRandomFruitCollectableUseCase = spawnRandomFruitCollectableUseCase;
    }

    public void Execute(CrateView crateView, int ammount)
    {
        Vector2 position = crateView.GlobalPosition;

        for (int i = 0; i < ammount; i++)
        {
            Optional<CollectableView> optionalCollectable = _spawnRandomFruitCollectableUseCase.Execute(true);

            bool hasCollectable = optionalCollectable.TryGet(out CollectableView collectableView);

            if (!hasCollectable)
            {
                continue;
            }

            collectableView.GlobalPosition = position;

            Vector2 randomDirection = new Vector2(
                _randomGenerator.NewFloat(-1, 1),
                _randomGenerator.NewFloat(-1, -0.5f)
            );
            Vector2 forceVector = new Vector2(
                randomDirection.X * _gameCratesConfiguration.HorizontalFruitSpawnForce, 
                randomDirection.Y * _gameCratesConfiguration.VerticalFruitSpawnForce
                );
            
            collectableView.ApplyForce(forceVector);
        }
    }
}