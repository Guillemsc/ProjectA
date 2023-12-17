using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class StartPlayerUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly IAsyncTaskRunner _asyncTaskRunner;

    public StartPlayerUseCase(
        PlayerViewData playerViewData,
        IAsyncTaskRunner asyncTaskRunner
    )
    {
        _playerViewData = playerViewData;
        _asyncTaskRunner = asyncTaskRunner;
    }

    public void Execute(bool enabled)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        async Task PlayAnimation(CancellationToken cancellationToken)
        {
            playerView.AnimatedSprite!.Play(PlayerAnimationState.Appear);
            playerView.AnimatedSprite!.Visible = true;
            
            await playerView.AnimatedSprite!.AwaitCompletition(cancellationToken);
            
            playerView.AnimationPlayer!.ProcessMode = Node.ProcessModeEnum.Inherit;
            playerView.CanUpdateMovement = true;
            playerView.CanMove = enabled;
        }

        _asyncTaskRunner.Run(PlayAnimation);
    }
}