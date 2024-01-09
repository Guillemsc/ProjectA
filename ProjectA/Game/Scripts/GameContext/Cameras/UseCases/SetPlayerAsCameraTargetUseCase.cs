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
    readonly PlayerViewData _playerViewData;
    readonly SetCameraTargetUseCase _setCameraTargetUseCase;

    public SetPlayerAsCameraTargetUseCase(
        PlayerViewData playerViewData, 
        SetCameraTargetUseCase setCameraTargetUseCase
        )
    {
        _playerViewData = playerViewData;
        _setCameraTargetUseCase = setCameraTargetUseCase;
    }
    
    public void Execute(bool invalidateState)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        _setCameraTargetUseCase.Execute(playerView, invalidateState);
    }
}