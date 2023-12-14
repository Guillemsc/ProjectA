using Game.GameContext.General.Datas;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Maps.UseCases;

public sealed class SpawnMapUseCase
{
    readonly GameMapsConfiguration _gameMapsConfiguration;
    readonly MapViewData _mapViewData;
    readonly GameGeneralViewData _gameGeneralViewData;

    public SpawnMapUseCase(
        GameMapsConfiguration gameMapsConfiguration, 
        MapViewData mapViewData, 
        GameGeneralViewData gameGeneralViewData
        )
    {
        _gameMapsConfiguration = gameMapsConfiguration;
        _mapViewData = mapViewData;
        _gameGeneralViewData = gameGeneralViewData;
    }

    public void Execute()
    {
        PackedScene mapPackedScene = _gameMapsConfiguration.MapsPrefabs![0];
        
        MapView mapView = mapPackedScene!.Instantiate<MapView>();
        mapView.SetParent(_gameGeneralViewData.PlayerParent);
        
        _mapViewData.MapView = mapView;
    }
}