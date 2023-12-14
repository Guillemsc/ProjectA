using Game.GameContext.VisualEffects.Configurations;
using Game.GameContext.VisualEffects.Datas;
using Game.GameContext.VisualEffects.Enums;

namespace Game.GameContext.VisualEffects.UseCases;

public sealed class RegisterVisualEffectsPrefabsUseCase
{
    readonly GameVisualEffectsConfiguration _gameVisualEffectsConfiguration;
    readonly VisualEffectsPrefabsData _visualEffectsPrefabsData;

    public RegisterVisualEffectsPrefabsUseCase(
        GameVisualEffectsConfiguration gameVisualEffectsConfiguration,
        VisualEffectsPrefabsData visualEffectsPrefabsData
        )
    {
        _gameVisualEffectsConfiguration = gameVisualEffectsConfiguration;
        _visualEffectsPrefabsData = visualEffectsPrefabsData;
    }

    public void Execute()
    {
        _visualEffectsPrefabsData.OneShotVisualEffectsPrefabs[OneShotVisualEffectType.CollectableCollected] =
            _gameVisualEffectsConfiguration.CollectableCollectedVisualEffectPrefab!;
    }
}