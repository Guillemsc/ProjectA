using Game.GameContext.Areas.Datas;
using Game.GameContext.Areas.Views;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Areas.UseCases;

public sealed class GetCurrentPlayerAreaUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly MapViewData _mapViewData;

    public GetCurrentPlayerAreaUseCase(
        PlayerViewData playerViewData,
        MapViewData mapViewData
        )
    {
        _playerViewData = playerViewData;
        _mapViewData = mapViewData;
    }

    public Optional<AreaView> Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return Optional<AreaView>.None;
        }

        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return Optional<AreaView>.None;
        }

        Vector2 position = playerView.GlobalPosition;

        AreaView? closestDistanceArea = null;
        float closestDistance = int.MaxValue;
        
        foreach (AreaView areaView in mapView.AreaViews!)
        {
            Rect2 bounds = areaView.Bounds!.GetGlobalRect();
            
            Vector2 boundsMin = bounds.GetMinPoint();
            Vector2 boundsMax = bounds.GetMaxPoint();

            float closestHorizontal = Mathf.Clamp(position.X, boundsMin.X, boundsMax.X);
            float closestVertical = Mathf.Clamp(position.Y, boundsMin.Y, boundsMax.Y);
            
            Vector2 closest = new Vector2(closestHorizontal, closestVertical);

            float distance = position.DistanceTo(closest);

            if (distance < closestDistance)
            {
                closestDistanceArea = areaView;
                closestDistance = distance;
            }
        }

        return closestDistanceArea ?? Optional<AreaView>.None;
    }
}