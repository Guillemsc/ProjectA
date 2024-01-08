using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.GameContext.VelocityBoosters.Views;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.VelocityBoosters.UseCases;

public sealed class WhenPlayerCollidedWithVelocityBoosterUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly ShakeCameraUseCase _shakeCameraUseCase;
    readonly PauseGameLogicSomeFramesUseCase _pauseGameLogicSomeFramesUseCase;

    public WhenPlayerCollidedWithVelocityBoosterUseCase(
        PlayerViewData playerViewData, 
        ShakeCameraUseCase shakeCameraUseCase, 
        PauseGameLogicSomeFramesUseCase pauseGameLogicSomeFramesUseCase
        )
    {
        _playerViewData = playerViewData;
        _shakeCameraUseCase = shakeCameraUseCase;
        _pauseGameLogicSomeFramesUseCase = pauseGameLogicSomeFramesUseCase;
    }
    
    public void Execute(VelocityBoosterView velocityBoosterView)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        ActionExtensions.CallDeferred(() => velocityBoosterView.Area2D!.Monitorable = false);
        
        Vector2 direction = MathExtensions.GetDirectionFromAngle(
            velocityBoosterView.GlobalRotationDegrees - 90
        );
        
        velocityBoosterView.AnimationPlayer!.NeedsToPlayCollected = true;
        
        playerView.MovementVelocity = direction * velocityBoosterView.BoostStrenght;
        
        _shakeCameraUseCase.Execute();
        _pauseGameLogicSomeFramesUseCase.Execute();
    }
}