using Game.GameContext.Player.Configurations;
using Game.GameContext.Player.Datas;
using Game.GameContext.Player.Enums;
using Game.GameContext.Player.Views;
using Godot;
using GUtils.Directions;
using GUtils.Extensions;
using GUtils.Time.Services;

namespace Game.GameContext.Player.UseCases;

public sealed class TickPlayerMovementUseCase
{
    readonly IDeltaTimeService _deltaTimeService;
    readonly PlayerViewData _playerViewData;
    readonly GamePlayerConfiguration _gamePlayerConfiguration;
    
    public TickPlayerMovementUseCase(
        IDeltaTimeService deltaTimeService, 
        PlayerViewData playerViewData, 
        GamePlayerConfiguration gamePlayerConfiguration
        )
    {
        _deltaTimeService = deltaTimeService;
        _playerViewData = playerViewData;
        _gamePlayerConfiguration = gamePlayerConfiguration;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        float delta = _deltaTimeService.PhysicsDeltaTime;
        
        Vector2 newVelocity = playerView.Velocity;
        
        newVelocity = HandleGravity(playerView, newVelocity, delta);
        newVelocity = HandleJump(playerView, newVelocity);
        newVelocity = HandleWall(playerView, newVelocity);
        newVelocity = HandleHorizontalMovement(playerView, newVelocity);

        playerView.Velocity = newVelocity;
        playerView.MoveAndSlide();
    }

    Vector2 HandleWall(PlayerView playerView, Vector2 newVelocity)
    {
        if (playerView.OnWall)
        {
            newVelocity.Y = Mathf.Min(newVelocity.Y, _gamePlayerConfiguration.VerticalMaxFallSpeedOnWall);
        }

        return newVelocity;
    }

    Vector2 HandleJump(PlayerView playerView, Vector2 newVelocity)
    {
        if (Input.IsActionJustPressed("ui_accept") && playerView.IsOnFloor())
        {
            newVelocity.Y = -_gamePlayerConfiguration.JumpVelocity;
        }

        return newVelocity;
    }

    Vector2 HandleGravity(PlayerView playerView, Vector2 newVelocity, float delta)
    {
        if (!playerView.IsOnFloor())
        {
            newVelocity.Y += _gamePlayerConfiguration.Gravity * delta;

            if (newVelocity.Y > 0f)
            {
                newVelocity.Y = Mathf.Min(newVelocity.Y, _gamePlayerConfiguration.VerticalMaxFallSpeed);
            }
        }

        playerView.OnAir = !playerView.IsOnFloor();
        playerView.OnAirState = newVelocity.Y < 0f ? PlayerOnAirState.Jump : PlayerOnAirState.Fall;
        
        return newVelocity;
    }

    Vector2 HandleHorizontalMovement(PlayerView playerView, Vector2 newVelocity)
    {
        Vector2 movementDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        if (movementDirection.X != 0f)
        {
            bool changedDirection =
                playerView.HorizontalDirection == HorizontalDirection.Right && movementDirection.X < 0f
                || playerView.HorizontalDirection == HorizontalDirection.Left && movementDirection.X > 0f;

            if (changedDirection)
            {
                newVelocity.X = 0f;
            }
            
            newVelocity.X += movementDirection.X * _gamePlayerConfiguration.HorizontalAcceleration;

            newVelocity.X = MathExtensions.Clamp(
                newVelocity.X,
                -_gamePlayerConfiguration.HorizontalMaxSpeed,
                _gamePlayerConfiguration.HorizontalMaxSpeed
            );
            
            playerView.HorizontalDirection = movementDirection.X > 0f ? HorizontalDirection.Right : HorizontalDirection.Left;
        }
        else
        {
            newVelocity.X = Mathf.MoveToward(playerView.Velocity.X, 0, _gamePlayerConfiguration.HorizontalDeceleration);
        }
        
        playerView.MovingHorizontally = newVelocity.X != 0f;

        return newVelocity;
    }
}