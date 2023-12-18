using Game.GameContext.Cameras.Datas;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using GUtils.Optionals;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.PositionProcessors;
using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetPlayerAsCameraTargetUseCase
{
    readonly ICameras2dService _cameras2dService;
    readonly PlayerViewData _playerViewData;

    public SetPlayerAsCameraTargetUseCase(
        ICameras2dService cameras2dService, 
        PlayerViewData playerViewData
        )
    {
        _cameras2dService = cameras2dService;
        _playerViewData = playerViewData;
    }
    
    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        FollowTargetCamera2dBehaviour followTarget = _cameras2dService.GetBehaviour<FollowTargetCamera2dBehaviour>().UnsafeGet();
        followTarget.SetTarget(playerView);
    }
}