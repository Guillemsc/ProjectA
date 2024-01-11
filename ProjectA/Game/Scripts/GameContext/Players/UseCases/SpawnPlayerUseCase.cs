using Game.GameContext.Connections.UseCases;
using Game.GameContext.Connections.Views;
using Game.GameContext.General.Configurations;
using Game.GameContext.General.Datas;
using Game.GameContext.Players.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Locations.Enums;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class SpawnPlayerUseCase
{
    readonly GameApplicationContextConfiguration _contextConfiguration;
    readonly GamePlayersConfiguration _gamePlayersConfiguration;
    readonly PlayerViewData _playerViewData;
    readonly GameGeneralViewData _gameGeneralViewData;
    readonly CanPlayerPlayAppearAnimationUseCase _canPlayerPlayAppearAnimationUseCase;
    readonly GetConnectionWithIdOrFirstUseCase _getConnectionWithIdOrFirstUseCase;
    readonly WhenPlayerStartedCollisionWithWallUseCase _whenPlayerStartedCollisionWithWallUseCase;
    readonly WhenPlayerStoppedCollisionWithWallUseCase _whenPlayerStoppedCollisionWithWallUseCase;
    readonly WhenPlayerStartedInteractionCollisionWithAreaUseCase _whenPlayerStartedInteractionCollisionWithAreaUseCase;
    readonly WhenPlayerBodyStartedCollisionUseCase _whenPlayerBodyStartedCollisionUseCase;

    public SpawnPlayerUseCase(
        GameApplicationContextConfiguration contextConfiguration,
        GamePlayersConfiguration gamePlayersConfiguration, 
        PlayerViewData playerViewData, 
        GameGeneralViewData gameGeneralViewData, 
        CanPlayerPlayAppearAnimationUseCase canPlayerPlayAppearAnimationUseCase,
        GetConnectionWithIdOrFirstUseCase getConnectionWithIdOrFirstUseCase,
        WhenPlayerStartedCollisionWithWallUseCase whenPlayerStartedCollisionWithWallUseCase,
        WhenPlayerStoppedCollisionWithWallUseCase whenPlayerStoppedCollisionWithWallUseCase, 
        WhenPlayerStartedInteractionCollisionWithAreaUseCase whenPlayerStartedInteractionCollisionWithAreaUseCase,
        WhenPlayerBodyStartedCollisionUseCase whenPlayerBodyStartedCollisionUseCase
        )
    {
        _contextConfiguration = contextConfiguration;
        _gamePlayersConfiguration = gamePlayersConfiguration;
        _playerViewData = playerViewData;
        _gameGeneralViewData = gameGeneralViewData;
        _canPlayerPlayAppearAnimationUseCase = canPlayerPlayAppearAnimationUseCase;
        _getConnectionWithIdOrFirstUseCase = getConnectionWithIdOrFirstUseCase;
        _whenPlayerStartedCollisionWithWallUseCase = whenPlayerStartedCollisionWithWallUseCase;
        _whenPlayerStoppedCollisionWithWallUseCase = whenPlayerStoppedCollisionWithWallUseCase;
        _whenPlayerStartedInteractionCollisionWithAreaUseCase = whenPlayerStartedInteractionCollisionWithAreaUseCase;
        _whenPlayerBodyStartedCollisionUseCase = whenPlayerBodyStartedCollisionUseCase;
    }

    public void Execute()
    {
        PlayerView playerView = _gamePlayersConfiguration.PlayerPrefab!.Instantiate<PlayerView>();
        playerView.SetParent(_gameGeneralViewData.Root);

        bool playerAppears = _canPlayerPlayAppearAnimationUseCase.Execute();
        
        playerView.AnimationPlayer!.HorizontalDirection = _contextConfiguration.PlayerDirection;
        playerView.AnimatedSprite!.Visible = !playerAppears;
        playerView.CanUpdateMovement = !playerAppears;
        
        Optional<ConnectionView> optionalConnectionView = _getConnectionWithIdOrFirstUseCase.Execute(
            _contextConfiguration.SpawnId
        );

        bool hasConnectionView = optionalConnectionView.TryGet(out ConnectionView connectionView);

        if (hasConnectionView)
        {
            playerView.GlobalPosition = connectionView.SpawnPosition!.GlobalPosition;
        }
        
        playerView.LeftWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Left));
        playerView.RightWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Right));
        
        playerView.LeftWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        playerView.RightWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        
        playerView.InteractionsDetector!.ConnectAreaEntered(_whenPlayerStartedInteractionCollisionWithAreaUseCase.Execute);
        
        playerView.CharacterBody2DCollisionCallbacks!.OnEnter += _whenPlayerBodyStartedCollisionUseCase.Execute;
        
        _playerViewData.PlayerView = playerView;
    }
}