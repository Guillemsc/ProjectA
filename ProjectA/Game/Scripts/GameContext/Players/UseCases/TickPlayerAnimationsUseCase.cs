using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Directions;
using GUtils.Locations.Enums;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class TickPlayerAnimationsUseCase
{
    readonly PlayerViewData _playerViewData;

    public TickPlayerAnimationsUseCase(
        PlayerViewData playerViewData
        )
    {
        _playerViewData = playerViewData;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        bool flip = playerView.HorizontalDirection == HorizontalDirection.Left;
        
        PlayerAnimationState state = PlayerAnimationState.Idle;

        if (playerView is { MovingHorizontally: true, OnAir: false })
        {
            state = PlayerAnimationState.Run;
        }
        else if(playerView is { OnAir: true, OnWall: true })
        {
            state = PlayerAnimationState.Wall;
            flip = playerView.OnWallLocation == HorizontalLocation.Left;
            playerView.HorizontalDirection = playerView.OnWallLocation == HorizontalLocation.Left
                ? HorizontalDirection.Right
                : HorizontalDirection.Left;
        }
        else if (playerView.OnAir)
        {
            state = playerView.OnAirState == PlayerOnAirState.Jump
                ? PlayerAnimationState.Jump
                : PlayerAnimationState.Fall;
        }

        AnimatedSprite2D animatedSprite2D = playerView.AnimatedSprite!;
        animatedSprite2D.Play(state);
        animatedSprite2D.FlipH = flip;
    }
}