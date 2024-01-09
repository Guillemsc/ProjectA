using Godot;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetCameraTargetUseCase
{
    readonly ICameras2dService _cameras2dService;

    public SetCameraTargetUseCase(
        ICameras2dService cameras2dService
    )
    {
        _cameras2dService = cameras2dService;
    }
    
    public void Execute(Node2D node2D, bool invalidateState)
    {
        FollowTargetCamera2dBehaviour followTarget = _cameras2dService.GetBehaviour<FollowTargetCamera2dBehaviour>().UnsafeGet();
        followTarget.SetTarget(node2D);
        
        if (invalidateState)
        {
            _cameras2dService.InvalidateState();   
        }
    }
}