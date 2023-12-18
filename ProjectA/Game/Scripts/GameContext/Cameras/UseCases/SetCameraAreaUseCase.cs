using Game.GameContext.Areas.Views;
using Game.GameContext.Cameras.Datas;
using GUtils.Optionals;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.PositionProcessors;
using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetCameraAreaUseCase
{
    readonly ICameras2dService _cameras2dService;
    readonly CameraBehavioursData _cameraBehavioursData;

    public SetCameraAreaUseCase(
        ICameras2dService cameras2dService,
        CameraBehavioursData cameraBehavioursData
    )
    {
        _cameras2dService = cameras2dService;
        _cameraBehavioursData = cameraBehavioursData;
    }
    
    public void Execute(AreaView areaView)
    {
        FollowZoomCamera2dBehaviour followZoom = _cameras2dService.GetBehaviour<FollowZoomCamera2dBehaviour>().UnsafeGet();
        followZoom.SetTargetZoom(areaView.CameraZoom);

        BoundsPosition2dProcessor boundsPosition2dProcessor = _cameraBehavioursData.BoundsProcessor.UnsafeGet();
        boundsPosition2dProcessor.SetBounds(areaView.Bounds!);
    }
}