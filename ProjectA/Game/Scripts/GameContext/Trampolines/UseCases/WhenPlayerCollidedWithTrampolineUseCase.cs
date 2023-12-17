using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.GameContext.Trampolines.Views;
using Godot;

namespace Game.GameContext.Trampolines.UseCases;

public sealed class WhenPlayerCollidedWithTrampolineUseCase
{
    readonly PlayerViewData _playerViewData;

    public WhenPlayerCollidedWithTrampolineUseCase(
        PlayerViewData playerViewData
        )
    {
        _playerViewData = playerViewData;
    }

    public void Execute(TrampolineView trampolineView)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        trampolineView.AnimationPlayer!.NeedsToPlayJump = true;
        
        Vector2 newVelocity = playerView.Velocity;
        newVelocity.Y = -trampolineView.BounceStrenght;
        playerView.Velocity = newVelocity;

        playerView.CanJump = true;
        playerView.CanDoubleJump = true;
    }
}