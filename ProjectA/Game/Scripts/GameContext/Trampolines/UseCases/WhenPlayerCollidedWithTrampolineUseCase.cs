using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.GameContext.Trampolines.Views;

namespace Game.GameContext.Trampolines.UseCases;

public sealed class WhenPlayerCollidedWithTrampolineUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly ShakeCameraUseCase _shakeCameraUseCase;

    public WhenPlayerCollidedWithTrampolineUseCase(
        PlayerViewData playerViewData, 
        ShakeCameraUseCase shakeCameraUseCase
        )
    {
        _playerViewData = playerViewData;
        _shakeCameraUseCase = shakeCameraUseCase;
    }

    public void Execute(TrampolineView trampolineView)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        trampolineView.AnimationPlayer!.NeedsToPlayJump = true;
        
        playerView.MovementVelocity.Y = -trampolineView.BounceStrenght;

        playerView.CanJump = true;
        playerView.CanDoubleJump = true;
        
        _shakeCameraUseCase.Execute();
    }
}