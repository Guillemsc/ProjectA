using System;
using System.Threading.Tasks;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.General.Configurations;
using Game.GameContext.General.Datas;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Godot;
using GUtils.DiscriminatedUnions;
using GUtils.Types;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Maps.UseCases;

public sealed class SpawnMapUseCase
{
    readonly GameApplicationContextConfiguration _gameApplicationContextConfiguration;
    readonly MapViewData _mapViewData;
    readonly GameGeneralViewData _gameGeneralViewData;

    public SpawnMapUseCase(
        GameApplicationContextConfiguration gameApplicationContextConfiguration, 
        MapViewData mapViewData, 
        GameGeneralViewData gameGeneralViewData
        )
    {
        _gameApplicationContextConfiguration = gameApplicationContextConfiguration;
        _mapViewData = mapViewData;
        _gameGeneralViewData = gameGeneralViewData;
    }

    public async Task Execute()
    {
        string toLoad = _gameApplicationContextConfiguration.MapToLoad;
        
        OneOf<PackedScene, ErrorMessage> result = await ResourceLoaderExtensions.LoadAsync<PackedScene>(
            toLoad
        );
        
        if (result.HasSecond)
        {
            return;
        }

        PackedScene packedScene = result.UnsafeGetFirst();
        
        MapView mapView = packedScene!.Instantiate<MapView>();
        mapView.SetParent(_gameGeneralViewData.Root);
        
        _mapViewData.MapView = mapView;
    }
}