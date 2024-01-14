using Game.GameContext.Players.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Game.ServicesContext.Time.Services;
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
    readonly IGameTimesService _gameTimesService;
    readonly PlayerViewData _playerViewData;
    readonly GamePlayersConfiguration _gamePlayersConfiguration;
    readonly StorePlayerPreviousPositionUseCase _storePlayerPreviousPositionUseCase;

    readonly ITimer _jumpResetTimer = new StopwatchTimer();
    
    public TickPlayerMovementUseCase(
        IGameTimesService gameTimesService, 
        PlayerViewData playerViewData, 
        GamePlayersConfiguration gamePlayersConfiguration, 
        StorePlayerPreviousPositionUseCase storePlayerPreviousPositionUseCase
        )
    {
        _gameTimesService = gameTimesService;
        _playerViewData = playerViewData;
        _gamePlayersConfiguration = gamePlayersConfiguration;
        _storePlayerPreviousPositionUseCase = storePlayerPreviousPositionUseCase;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        if (!playerView.CanUpdateMovement)
        {
            playerView.Velocity = Vector2.Zero;
            return;
        }
        
        float scale = _gameTimesService.PhysicsTimeContext.TimeScale;
        float delta = _gameTimesService.PhysicsTimeContext.DeltaTime;
        
        Vector2 newVelocity = playerView.MovementVelocity;
        
        newVelocity = HandleGravity(playerView, newVelocity, delta);
        newVelocity = HandleWall(playerView, newVelocity, delta);
        newVelocity = HandleJump(playerView, newVelocity, delta);
        newVelocity = HandleHorizontalMovement(playerView, newVelocity, delta);
        
        HandleCrouchAndLookUp(playerView);
        HandleUncontrolledSpeed(playerView, delta);

        playerView.MovementVelocity = newVelocity + playerView.UncontrolledSpeed;
        
        playerView.Velocity = playerView.MovementVelocity * scale;
        playerView.MoveAndSlide();
        
        _storePlayerPreviousPositionUseCase.Execute(playerView);
    }

    Vector2 HandleWall(PlayerView playerView, Vector2 newVelocity, float delta)
    {
        float deltaVerticalMaxFallSpeedOnWall = _gamePlayersConfiguration.VerticalMaxFallSpeedOnWall * delta;
        
        if (playerView.AnimationPlayer!.OnWall && !playerView.IsOnFloor())
        {
            newVelocity.Y = Mathf.Min(newVelocity.Y, deltaVerticalMaxFallSpeedOnWall);

            ResetJumps(playerView);
        }

        return newVelocity;
    }

    Vector2 HandleJump(PlayerView playerView, Vector2 newVelocity, float delta)
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
            float deltaJumpVelocity = _gamePlayersConfiguration.JumpVelocity * delta;
            
            newVelocity.Y = -deltaJumpVelocity;

            bool needsToAddSideUncontrolledSpeedWhenOnWall = playerView.AnimationPlayer!.OnWall && !playerView.IsOnFloor();
            
            if (needsToAddSideUncontrolledSpeedWhenOnWall)
            {
                float wallForceDirection = -playerView.AnimationPlayer!.OnWallLocation.Direction();
  
                playerView.UncontrolledSpeed.X += (
                    wallForceDirection *
                    deltaJumpVelocity *
                    _gamePlayersConfiguration.UncontrolledHorizontalJumpVelocityMultiplier
                );
            }
        }

        return newVelocity;
    }

    Vector2 HandleGravity(PlayerView playerView, Vector2 newVelocity, float delta)
    {
        bool wasOnAir = playerView.AnimationPlayer!.OnAir;
        
        if (!playerView.IsOnFloor())
        {
            float deltaGravity = _gamePlayersConfiguration.Gravity * delta;
            
            newVelocity.Y += deltaGravity;

            if (newVelocity.Y > 0f)
            {
                newVelocity.Y = Mathf.Min(newVelocity.Y, _gamePlayersConfiguration.VerticalMaxFallSpeed);
            }
        }
        else if(wasOnAir)
        {
            newVelocity.Y = Mathf.Min(0, newVelocity.Y);
        }

        if (playerView.IsOnCeiling())
        {
            newVelocity.Y = Mathf.Max(0, newVelocity.Y);
        }

        playerView.AnimationPlayer!.OnAir = !playerView.IsOnFloor();
        playerView.AnimationPlayer!.OnAirState = newVelocity.Y < 0f ? PlayerOnAirState.Jump : PlayerOnAirState.Fall;
        
        return newVelocity;
    }

    Vector2 HandleHorizontalMovement(PlayerView playerView, Vector2 newVelocity, float delta)
    {
        int horizontalDirection = 0;

        if (Input.IsActionPressed("ui_left"))
        {
            horizontalDirection -= 1;
        }
        
        if (Input.IsActionPressed("ui_right"))
        {
            horizontalDirection += 1;
        }

        bool thereWasHorizontalMovement = horizontalDirection != 0 && playerView.CanMove;

        float velocityToAdd = 0f;

        if (thereWasHorizontalMovement)
        {
            bool changedDirection =
                playerView.AnimationPlayer!.HorizontalDirection == HorizontalDirection.Right && horizontalDirection < 0
                || playerView.AnimationPlayer!.HorizontalDirection == HorizontalDirection.Left &&
                horizontalDirection > 0;

            float deltaHorizontalAcceleration = _gamePlayersConfiguration.HorizontalAcceleration * delta;
            
            velocityToAdd = horizontalDirection * deltaHorizontalAcceleration;
            float finalVelocity = newVelocity.X + velocityToAdd;

            float horizontalMaxSpeed = playerView.IsOnFloor()
                ? _gamePlayersConfiguration.HorizontalMaxSpeed
                : _gamePlayersConfiguration.HorizontalMaxSpeedOnAir;

            if (Mathf.Abs(finalVelocity) > horizontalMaxSpeed)
            {
                velocityToAdd = horizontalDirection * Mathf.Max(0, horizontalMaxSpeed - Mathf.Abs(newVelocity.X));
            }

            if (changedDirection && Mathf.Abs(finalVelocity) < horizontalMaxSpeed)
            {
                newVelocity.X = 0f;
            }

            playerView.AnimationPlayer!.HorizontalDirection =
                horizontalDirection > 0 ? HorizontalDirection.Right : HorizontalDirection.Left;
        }

        if (velocityToAdd == 0)
        {
            float deltaHorizontalDeceleration = _gamePlayersConfiguration.HorizontalDeceleration * delta;
            newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, deltaHorizontalDeceleration);
        }
        else
        {
            newVelocity.X += velocityToAdd;
        }

        playerView.AnimationPlayer!.MovingHorizontally = newVelocity.X != 0f;

        return newVelocity;
    }

    void HandleCrouchAndLookUp(PlayerView playerView)
    {
        int verticalDirection = 0;

        if (Input.IsActionPressed("ui_up"))
        {
            verticalDirection += 1;
        }
        
        if (Input.IsActionPressed("ui_down"))
        {
            verticalDirection -= 1;
        }

        playerView.AnimationPlayer!.Crouching = verticalDirection < 0;
        playerView.AnimationPlayer!.LookingUp = verticalDirection > 0;
    }
    
    void HandleUncontrolledSpeed(PlayerView playerView, float delta)
    {
        float deltaHorizontalDeceleration = _gamePlayersConfiguration.HorizontalDeceleration * delta;
        
        playerView.UncontrolledSpeed.X = Mathf.MoveToward(
            playerView.UncontrolledSpeed.X,
            0,
            deltaHorizontalDeceleration
        );
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