using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cameras.Enums;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Pause.Enums;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtils.Tasks.Runners;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class KillPlayerUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly ShakeCameraUseCase _shakeCameraUseCase;
    readonly PauseGameLogicSomeFramesUseCase _pauseGameLogicSomeFramesUseCase;

    public KillPlayerUseCase(
        PlayerViewData playerViewData,
        ShakeCameraUseCase shakeCameraUseCase, 
        PauseGameLogicSomeFramesUseCase pauseGameLogicSomeFramesUseCase
        )
    {
        _playerViewData = playerViewData;
        _shakeCameraUseCase = shakeCameraUseCase;
        _pauseGameLogicSomeFramesUseCase = pauseGameLogicSomeFramesUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        _shakeCameraUseCase.Execute(ShakeCameraStrenght.Strong);
        _pauseGameLogicSomeFramesUseCase.Execute(PauseFramesDuration.VeryLong);

        playerView.AnimationPlayer!.ProcessMode = Node.ProcessModeEnum.Disabled;
        playerView.CanUpdateMovement = false;
        playerView.CanMove = false;

        playerView.Modulate = new Color(1000, 1000, 1000);
            
        if (playerView.PreviousPositions.Count > 0)
        {
            Vector2 previousPosition = playerView.PreviousPositions[0];
            Vector2 commingDirection = (previousPosition - playerView.GlobalPosition).Normalized();
            Vector2 finalPosition = playerView.GlobalPosition + commingDirection * 40;
            
            await playerView.TweenGlobalPosition(finalPosition, 0.3f)
                .SetEasing(Easing.OutCubic)
                .PlayAsync(cancellationToken);
        }
        
        if(cancellationToken.IsCancellationRequested) return;

        playerView.Modulate = new Color(1, 1, 1);
            
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Disappear);
        await playerView.AnimatedSprite!.AwaitCompletition(cancellationToken);
        
        if(cancellationToken.IsCancellationRequested) return;
            
        playerView.AnimatedSprite!.Visible = false;
    }
}