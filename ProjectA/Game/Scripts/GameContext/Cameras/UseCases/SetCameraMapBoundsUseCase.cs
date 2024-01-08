using Game.GameContext.Areas.Views;
using Game.GameContext.Cameras.Datas;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.Views;
using Godot;
using GUtilsGodot.Cameras.Behaviours;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetCameraMapBoundsUseCase
{
    readonly CameraBehavioursData _cameraBehavioursData;
    readonly MapViewData _mapViewData;

    public SetCameraMapBoundsUseCase(
        CameraBehavioursData cameraBehavioursData, 
        MapViewData mapViewData
        )
    {
        _cameraBehavioursData = cameraBehavioursData;
        _mapViewData = mapViewData;
    }

    public void Execute()
    {
        bool hasMap = _mapViewData.MapView.TryGet(out MapView mapView);

        if (!hasMap)
        {
            return;
        }

        Rect2 rect = new Rect2();

        foreach (AreaView areaView in mapView.AreaViews!)
        {
            Rect2 boundsRect = areaView.Bounds!.GetGlobalRect();

            rect = rect.Merge(boundsRect);
        }
        
        BoundsConfinementCamera2dBehaviour boundsConfinementCamera2dBehaviour = _cameraBehavioursData.BoundsConfinementBehaviour.UnsafeGet();
        boundsConfinementCamera2dBehaviour.SetBounds(rect);
    }
}