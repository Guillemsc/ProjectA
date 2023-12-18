using Game.GameContext.Connections.Views;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using GUtils.Optionals;

namespace Game.GameContext.Connections.UseCases;

public sealed class GetConnectionWithIdUseCase
{
    readonly MapViewData _mapViewData;

    public GetConnectionWithIdUseCase(
        MapViewData mapViewData
        )
    {
        _mapViewData = mapViewData;
    }

    public Optional<ConnectionView> Execute(string id)
    {
        bool hasMapView = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMapView)
        {
            return Optional<ConnectionView>.None;
        }
        
        foreach (ConnectionView connectionView in mapView.ConnectionViews!)
        {
            bool isId = string.Equals(connectionView.SpawnId, id);

            if (isId)
            {
                return connectionView;
            }
        }
        
        return Optional<ConnectionView>.None;
    }
}