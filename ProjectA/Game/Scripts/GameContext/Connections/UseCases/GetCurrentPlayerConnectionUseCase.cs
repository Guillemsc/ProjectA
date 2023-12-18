using Game.GameContext.Connections.Views;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Optionals;

namespace Game.GameContext.Connections.UseCases;

public sealed class GetCurrentPlayerConnectionUseCase
{
    readonly MapViewData _mapViewData;
    readonly PlayerViewData _playerViewData;

    public GetCurrentPlayerConnectionUseCase(
        MapViewData mapViewData, 
        PlayerViewData playerViewData
    )
    {
        _mapViewData = mapViewData;
        _playerViewData = playerViewData;
    }
    
    public Optional<ConnectionView> Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return Optional<ConnectionView>.None;
        }

        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return Optional<ConnectionView>.None;
        }

        Vector2 position = playerView.GlobalPosition;
        
        foreach (ConnectionView connectionView in mapView.ConnectionViews!)
        {
            Rect2 bounds = connectionView.Bounds!.GetGlobalRect();

            bool isInside = bounds.HasPoint(position);

            if (isInside)
            {
                return connectionView;
            }
        }
        
        return Optional<ConnectionView>.None;
    }
}