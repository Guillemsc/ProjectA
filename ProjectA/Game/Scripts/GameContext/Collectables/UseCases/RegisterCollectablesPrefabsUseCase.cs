using Game.GameContext.Collectables.Configurations;
using Game.GameContext.Collectables.Datas;

namespace Game.GameContext.Collectables.UseCases;

public sealed class RegisterCollectablesPrefabsUseCase
{
    readonly GameCollectablesConfiguration _gameCollectablesConfiguration;
    readonly CollectablesPrefabsData _collectablesPrefabsData;

    public RegisterCollectablesPrefabsUseCase(
        GameCollectablesConfiguration gameCollectablesConfiguration, 
        CollectablesPrefabsData collectablesPrefabsData
        )
    {
        _gameCollectablesConfiguration = gameCollectablesConfiguration;
        _collectablesPrefabsData = collectablesPrefabsData;
    }

    public void Execute()
    {
        _collectablesPrefabsData.FruitCollectablesPrefabs.Add(_gameCollectablesConfiguration.ApplePrefab!);
    }
}