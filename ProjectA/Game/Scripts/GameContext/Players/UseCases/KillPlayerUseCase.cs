using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class KillPlayerUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly IAsyncTaskRunner _asyncTaskRunner;

    public KillPlayerUseCase(
        PlayerViewData playerViewData,
        IAsyncTaskRunner asyncTaskRunner
    )
    {
        _playerViewData = playerViewData;
        _asyncTaskRunner = asyncTaskRunner;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        async Task PlayAnimation(CancellationToken cancellationToken)
        {
            playerView.AnimationPlayer!.ProcessMode = Node.ProcessModeEnum.Disabled;
            playerView.CanUpdateMovement = false;
            playerView.CanMove = false;

            playerView.Modulate = new Color(1000, 1000, 1000);
            
            if (playerView.PreviousPositions.Count > 0)
            {
                Vector2 previousPosition = playerView.PreviousPositions[0];
                Vector2 commingDirection = (previousPosition - playerView.GlobalPosition).Normalized();
                Vector2 finalPosition = playerView.GlobalPosition + commingDirection * 50;
            
                await playerView.TweenGlobalPosition(finalPosition, 0.5f)
                    .SetEasing(Easing.InOutCubic)
                    .PlayAsync(cancellationToken);
            }

            playerView.Modulate = new Color(1, 1, 1);
            
            playerView.AnimatedSprite!.Play(PlayerAnimationState.Disappear);
            await playerView.AnimatedSprite!.AwaitCompletition(cancellationToken);
            
            playerView.AnimatedSprite!.Visible = false;
        }

        _asyncTaskRunner.Run(PlayAnimation);
    }
}