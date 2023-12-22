using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class TryPlayMapOptionalStartingCinematicUseCase
{
    readonly MapViewData _mapViewData;
    readonly PlayCinematicUseCase _playCinematicUseCase;

    public TryPlayMapOptionalStartingCinematicUseCase(
        MapViewData mapViewData,
        PlayCinematicUseCase playCinematicUseCase
        )
    {
        _mapViewData = mapViewData;
        _playCinematicUseCase = playCinematicUseCase;
    }

    public void Execute()
    {
        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return;
        }

        if (mapView.OptionalStartingCinematic == null)
        {
            return;
        }
        
        _playCinematicUseCase.Execute(mapView.OptionalStartingCinematic);
    }
}