using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Directions;
using GUtils.Locations.Enums;

namespace Game.GameContext.Players.UseCases;

public sealed class TickPlayerFlipStateUseCase
{
    readonly PlayerViewData _playerViewData;

    public TickPlayerFlipStateUseCase(
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

        bool flip = playerView.AnimationPlayer!.HorizontalDirection == HorizontalDirection.Left;

        if(playerView.AnimationPlayer is { OnAir: true, OnWall: true })
        {
            flip = playerView.AnimationPlayer.OnWallLocation == HorizontalLocation.Left;
            playerView.AnimationPlayer.HorizontalDirection = playerView.AnimationPlayer.OnWallLocation == HorizontalLocation.Left
                ? HorizontalDirection.Right
                : HorizontalDirection.Left;
        }
        
        AnimatedSprite2D animatedSprite2D = playerView.AnimatedSprite!;

        animatedSprite2D.FlipH = flip;
    }
}