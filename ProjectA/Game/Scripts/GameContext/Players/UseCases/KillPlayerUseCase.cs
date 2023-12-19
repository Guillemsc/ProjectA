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

    public KillPlayerUseCase(
        PlayerViewData playerViewData
    )
    {
        _playerViewData = playerViewData;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

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

        playerView.Modulate = new Color(1, 1, 1);
            
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Disappear);
        await playerView.AnimatedSprite!.AwaitCompletition(cancellationToken);
            
        playerView.AnimatedSprite!.Visible = false;
    }
}