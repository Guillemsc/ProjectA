using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.ServicesContext.Time.Services;
using Godot;
using GUtils.Directions;
using GUtils.Locations.Enums;

namespace Game.GameContext.Players.UseCases;

public sealed class TickPlayerFlipStateUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly PlayerViewData _playerViewData;
    
    public TickPlayerFlipStateUseCase(
        IGameTimesService gameTimesService,
        PlayerViewData playerViewData
        )
    {
        _gameTimesService = gameTimesService;
        _playerViewData = playerViewData;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        if (_gameTimesService.PhysicsTimeContext.TimeScale == 0f)
        {
            return;
        }
        
        if (!playerView.CanUpdateMovement)
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