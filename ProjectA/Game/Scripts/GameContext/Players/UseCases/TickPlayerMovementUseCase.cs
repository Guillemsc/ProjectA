using Game.GameContext.Players.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Directions;
using GUtils.Extensions;
using GUtils.Locations.Extensions;
using GUtils.Time.Extensions;
using GUtils.Time.Services;
using GUtils.Time.Timers;

namespace Game.GameContext.Players.UseCases;

public sealed class TickPlayerMovementUseCase
{
    readonly IDeltaTimeService _deltaTimeService;
    readonly PlayerViewData _playerViewData;
    readonly GamePlayersConfiguration _gamePlayersConfiguration;

    readonly ITimer _jumpResetTimer = new StopwatchTimer();
    
    public TickPlayerMovementUseCase(
        IDeltaTimeService deltaTimeService, 
        PlayerViewData playerViewData, 
        GamePlayersConfiguration gamePlayersConfiguration
        )
    {
        _deltaTimeService = deltaTimeService;
        _playerViewData = playerViewData;
        _gamePlayersConfiguration = gamePlayersConfiguration;
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
        newVelocity = HandleWall(playerView, newVelocity);
        newVelocity = HandleJump(playerView, newVelocity);
        newVelocity = HandleHorizontalMovement(playerView, newVelocity);
        
        HandleUncontrolledSpeed(playerView);

        playerView.Velocity = newVelocity + playerView.UncontrolledSpeed;
        playerView.MoveAndSlide();
    }

    void HandleUncontrolledSpeed(PlayerView playerView)
    {
        playerView.UncontrolledSpeed.X = Mathf.MoveToward(
            playerView.UncontrolledSpeed.X,
            0,
            _gamePlayersConfiguration.HorizontalDeceleration
        );
    }

    Vector2 HandleWall(PlayerView playerView, Vector2 newVelocity)
    {
        if (playerView.AnimationPlayer!.OnWall && !playerView.IsOnFloor())
        {
            newVelocity.Y = Mathf.Min(newVelocity.Y, _gamePlayersConfiguration.VerticalMaxFallSpeedOnWall);

            ResetJumps(playerView);
        }

        return newVelocity;
    }

    Vector2 HandleJump(PlayerView playerView, Vector2 newVelocity)
    {
        if (playerView.IsOnFloor())
        {
            ResetJumps(playerView);
        }

        bool jumpInput = Input.IsActionJustPressed("ui_accept");

        bool thereWasJumpInput = jumpInput && playerView.CanMove;
        
        if (!thereWasJumpInput)
        {
            return newVelocity;
        }
        
        bool performJump = false;

        if (playerView.CanJump)
        {
            playerView.CanJump = false;
            performJump = true;
        }
        else if(playerView.CanDoubleJump)
        {
            playerView.CanDoubleJump = false;
            playerView.AnimationPlayer!.NeedsToPlayDoubleJump = true;
            performJump = true;  
        }  

        if (performJump)
        {
            newVelocity.Y = -_gamePlayersConfiguration.JumpVelocity;

            bool needsToAddSideUncontrolledSpeedWhenOnWall = playerView.AnimationPlayer!.OnWall && !playerView.IsOnFloor();
            
            if (needsToAddSideUncontrolledSpeedWhenOnWall)
            {
                float wallForceDirection = -playerView.AnimationPlayer!.OnWallLocation.Direction();
                
                playerView.UncontrolledSpeed.X += (
                    wallForceDirection *
                    _gamePlayersConfiguration.JumpVelocity *
                    _gamePlayersConfiguration.UncontrolledHorizontalJumpVelocityMultiplier
                );
            }
        }

        return newVelocity;
    }

    Vector2 HandleGravity(PlayerView playerView, Vector2 newVelocity, float delta)
    {
        if (!playerView.IsOnFloor())
        {
            newVelocity.Y += _gamePlayersConfiguration.Gravity * delta;

            if (newVelocity.Y > 0f)
            {
                newVelocity.Y = Mathf.Min(newVelocity.Y, _gamePlayersConfiguration.VerticalMaxFallSpeed);
            }
        }

        playerView.AnimationPlayer!.OnAir = !playerView.IsOnFloor();
        playerView.AnimationPlayer!.OnAirState = newVelocity.Y < 0f ? PlayerOnAirState.Jump : PlayerOnAirState.Fall;
        
        return newVelocity;
    }

    Vector2 HandleHorizontalMovement(PlayerView playerView, Vector2 newVelocity)
    {
        Vector2 movementDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        bool thereWasHorizontalMovement = movementDirection.X != 0f && playerView.CanMove;
        
        if (thereWasHorizontalMovement)
        {
            bool changedDirection =
                playerView.AnimationPlayer!.HorizontalDirection == HorizontalDirection.Right && movementDirection.X < 0f
                || playerView.AnimationPlayer!.HorizontalDirection == HorizontalDirection.Left && movementDirection.X > 0f;

            if (changedDirection)
            {
                newVelocity.X = 0f;
            }
            
            newVelocity.X += movementDirection.X * _gamePlayersConfiguration.HorizontalAcceleration;

            float horizontalMaxSpeed = playerView.IsOnFloor()
                ? _gamePlayersConfiguration.HorizontalMaxSpeed
                : _gamePlayersConfiguration.HorizontalMaxSpeedOnAir;

            newVelocity.X = MathExtensions.Clamp(
                newVelocity.X,
                -horizontalMaxSpeed,
                horizontalMaxSpeed
            );
            
            playerView.AnimationPlayer!.HorizontalDirection = movementDirection.X > 0f ? HorizontalDirection.Right : HorizontalDirection.Left;
        }
        else
        {
            newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, _gamePlayersConfiguration.HorizontalDeceleration);
        }
        
        playerView.AnimationPlayer!.MovingHorizontally = newVelocity.X != 0f;

        return newVelocity;
    }

    void ResetJumps(PlayerView playerView)
    {
        if (!_jumpResetTimer.HasReachedOrNotStarted(_gamePlayersConfiguration.JumpsResetMinSeconds.ToSeconds()))
        {
            return;
        }
        
        playerView.CanJump = true;
        playerView.CanDoubleJump = true;
        
        _jumpResetTimer.Restart();
    }
}