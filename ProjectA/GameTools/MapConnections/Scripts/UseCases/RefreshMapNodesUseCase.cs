using System.Collections.Generic;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Views;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using GUtils.DiscriminatedUnions;
using GUtils.Optionals;
using GUtils.Types;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class RefreshMapNodesUseCase
{
    readonly GameMapsConfiguration _gameMapsConfiguration;
    readonly MapNodesData _mapNodesData;
    readonly AddNodeUseCase _addNodeUseCase;

    public RefreshMapNodesUseCase(
        GameMapsConfiguration gameMapsConfiguration, 
        MapNodesData mapNodesData, 
        AddNodeUseCase addNodeUseCase
        )
    {
        _gameMapsConfiguration = gameMapsConfiguration;
        _mapNodesData = mapNodesData;
        _addNodeUseCase = addNodeUseCase;
    }

    public void Execute()
    {
        List<string> toCheck = new();

        foreach (MapConnectionNodeView mapConnectionNodeView  in _mapNodesData.MapConnectionNodeViews)
        {
            toCheck.Add(mapConnectionNodeView.MapPrefab.ResourcePath);
        }

        for (int i = 0; i < _gameMapsConfiguration.MapConfigurations!.Count; i++)
        {
            MapConfiguration mapConfiguration = _gameMapsConfiguration.MapConfigurations[i];
            
            bool exists = toCheck.Contains(mapConfiguration.Map!);

            if (exists)
            {
                toCheck.Remove(mapConfiguration.Map!);
                continue;
            }
            
            OneOf<PackedScene, ErrorMessage> result = ResourceLoaderExtensions.Load<PackedScene>(mapConfiguration.Map!);

            if (!result.HasFirst)
            {
                continue;
            }

            PackedScene packedScene = result.UnsafeGetFirst();
                
            Optional<MapView> optionalMapView = packedScene.SafeInstantiate<MapView>();

            bool hasMapView = optionalMapView.TryGet(out MapView mapView);

            if (!hasMapView)
            {
                continue;
            }
                
            _addNodeUseCase.Execute(mapConfiguration, packedScene, mapView);
        }
    }
}