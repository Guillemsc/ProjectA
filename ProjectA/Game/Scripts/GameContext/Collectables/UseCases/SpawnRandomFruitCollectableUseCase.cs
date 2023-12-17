using Game.GameContext.Collectables.Datas;
using Game.GameContext.Collectables.Views;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Godot;
using GUtils.Optionals;
using GUtils.Randomization.Extensions;
using GUtils.Randomization.Generators;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Collectables.UseCases;

public sealed class SpawnRandomFruitCollectableUseCase
{
    readonly CollectablesPrefabsData _collectablesPrefabsData;
    readonly MapViewData _mapViewData;
    readonly IRandomGenerator _randomGenerator;

    public SpawnRandomFruitCollectableUseCase(
        CollectablesPrefabsData collectablesPrefabsData, 
        MapViewData mapViewData,
        IRandomGenerator randomGenerator
        )
    {
        _collectablesPrefabsData = collectablesPrefabsData;
        _mapViewData = mapViewData;
        _randomGenerator = randomGenerator;
    }

    public Optional<CollectableView> Execute(bool withPhysics)
    {
        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return Optional<CollectableView>.None;
        }
        
        bool couldGet = _randomGenerator.TryGetRandom(
            _collectablesPrefabsData.FruitCollectablesPrefabs,
            out PackedScene? packedScene
        );

        if (!couldGet)
        {
            return Optional<CollectableView>.None;
        }

        CollectableView collectableView = packedScene!.Instantiate<CollectableView>();
        collectableView.Freeze = !withPhysics;
            
        collectableView.SetParent(mapView);
        
        return collectableView;
    }
}