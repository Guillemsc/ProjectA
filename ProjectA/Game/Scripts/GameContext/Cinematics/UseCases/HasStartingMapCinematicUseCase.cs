using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class HasStartingMapCinematicUseCase
{
    readonly MapViewData _mapViewData;

    public HasStartingMapCinematicUseCase(
        MapViewData mapViewData
    )
    {
        _mapViewData = mapViewData;
    }
    
    public bool Execute()
    {
        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return false;
        }

        return mapView.OptionalStartingCinematic != null;
    }
}